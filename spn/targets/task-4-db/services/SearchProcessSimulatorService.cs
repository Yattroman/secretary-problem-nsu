using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using spn.targets.common.models;
using spn.targets.common.models.interfaces;
using spn.targets.common.util;
using spn.targets.task_4_db.entities;

namespace spn.targets.task_4_db.services;

public class SearchProcessSimulatorService : ISearchProcessSimulatorService
{
    private readonly IDbSearchLoveService _dbSearchLoveService;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IContendersFactory _contendersFactory;
    private readonly ILogger<SearchProcessSimulatorService> _logger;
    private readonly IConfiguration _configuration;

    private readonly int _startContendersNumber;
    private readonly List<RatedContender> _visitedContenders;

    public SearchProcessSimulatorService(IDbSearchLoveService dbSearchLoveService, IHall hall, IPrincess princess,
        ILogger<SearchProcessSimulatorService> logger, IConfiguration configuration,
        IContendersFactory contendersFactory)
    {
        _dbSearchLoveService = dbSearchLoveService;
        _hall = hall;
        _princess = princess;
        _contendersFactory = contendersFactory;
        _logger = logger;
        _configuration = configuration;

        _startContendersNumber = configuration.GetValue<int>("SearchLoveParams:DefaultStartContendersNumber");
        _visitedContenders = new List<RatedContender>();
    }

    public void SimulateSearchProcess(int triesNumber)
    {
        _dbSearchLoveService.DeleteAllSearchLoveTries();

        var particularFailTries = 0;
        var fullSuccessTries = 0;
        var fullFailureTries = 0;

        for (int i = 0; i < triesNumber; i++)
        {
            CleanupEnvironmentBeforeNextIteration();

            InitHall(UsingType.UpdateBase, new SearchLoveTry());

            var princessHappinessLevel = SearchLove();
            var nameSuffix = CreateNameSuffix
                (princessHappinessLevel, particularFailTries++, fullSuccessTries++, fullFailureTries++);

            _dbSearchLoveService
                .SaveSearchLoveTry($"Try[{i}]{nameSuffix}", _visitedContenders, princessHappinessLevel);
        }
    }

    private string CreateNameSuffix(int searchResult, int particularFailTries, int fullSuccessTries,
        int fullFailureTries)
    {
        var suffix = searchResult switch
        {
            IChoiceStrategy.ParticularFailure => $"_PF[{particularFailTries}]",
            IChoiceStrategy.FullFailure => $"_FF[{fullFailureTries}]",
            IChoiceStrategy.FullSuccess => $"_FS[{fullSuccessTries}]",
            _ => ""
        };

        return suffix;
    }

    public int RerunSearchProcessTryByName(string name, bool toPrint)
    {
        CleanupEnvironmentBeforeNextIteration();

        var searchLoveTry = _dbSearchLoveService.GetSearchLoveTryByName(name);
        if (searchLoveTry == null)
        {
            _logger.LogInformation($"There are no love search try with name: {name}.");
            return -1;
        }

        InitHall(UsingType.RecallSearches, searchLoveTry);
        var princessHappinessLevel = SearchLove();

        if (toPrint)
        {
            PrintResultedList(princessHappinessLevel, name);
        }

        return princessHappinessLevel;
    }

    public double GetAverageSearchResultValue(bool toPrint)
    {
        var averageValue = _dbSearchLoveService.GetAllSearchLoveTriesAverageValue();
        
        if (toPrint)
        {
            PrintAverageValue(averageValue);
        }

        return averageValue;
    }

    public List<SearchLoveTry> GetCurrentLoveSearchesStatus()
    {
        return _dbSearchLoveService.GetAllSearchLoveTries();
    }

    private void CleanupEnvironmentBeforeNextIteration()
    {
        _hall.ClearHall();
        _princess.PrepareToNextSearch();
        _visitedContenders.Clear();
    }

    private void InitHall(UsingType usingType, SearchLoveTry searchLoveTry)
    {
        if (usingType == UsingType.UpdateBase)
        {
            _contendersFactory
                .CreateRatedContenders(ContenderFactory.NetGeneration, _startContendersNumber)
                .ForEach((contender) => _hall.AddNewContender(contender));
        }
        else if (usingType == UsingType.RecallSearches && searchLoveTry.Contenders != null)
        {
            searchLoveTry.Contenders
                .Select(ce => new RatedContender(ce.FirstName, ce.SecondName, ce.Rating))
                .ToList()
                .ForEach(contender => _hall.AddNewContender(contender));
        }
    }

    private int SearchLove()
    {
        RatedContender? love = null;
        var orderCounter = 1;

        while (!_hall.IsNoContendersInHall())
        {
            var currentContender = _hall.ReturnNextContender();
            var response = _princess.CheckContender(currentContender);
            currentContender.OrderNumber = orderCounter++;

            _visitedContenders.Add(currentContender);

            if (response.Contender != null)
            {
                love = response.Contender;
                break;
            }
        }

        var princessHappinessLevel = _princess.CheckChoice(love);

        return princessHappinessLevel;
    }

    private void PrintResultedList(int choiceResult, string tryName)
    {
        using StreamWriter file = new($"{tryName}.txt");
        for (int i = 0; i < _visitedContenders.Count; i++)
        {
            file.WriteLineAsync($"{i + 1}: {_visitedContenders[i].GetDetails()}");
        }

        file.WriteLineAsync("--------------------");
        file.WriteLineAsync($"Choice result: {choiceResult}");
    }

    private void PrintAverageValue(double averageValue)
    {
        using StreamWriter file = new($"averageValue.txt");
        file.WriteLineAsync($"Average value: {averageValue}");
    }
}