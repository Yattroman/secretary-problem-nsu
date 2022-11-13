using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using spn.targets.common.models;
using spn.targets.common.models.interfaces;
using spn.targets.common.util;

namespace spn.targets.task_2_di_containers;

public class SearchProcHosted : BackgroundService
{
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IContendersFactory _contendersFactory;

    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<SearchProcHosted> _logger;

    private readonly int _startContendersNumber;
    private readonly List<RatedContender> _visitedContenders;

    private const int WaitingTime = 1000;

    public SearchProcHosted(IHall hall, IPrincess princess,
        IContendersFactory contendersFactory, ILogger<SearchProcHosted> logger,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        _hall = hall;
        _princess = princess;
        _contendersFactory = contendersFactory;

        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;

        _startContendersNumber = 100;
        _visitedContenders = new List<RatedContender>();
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start searching contenders by Princess");
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop searching contenders by Princess");
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await StartSearchingLove();
    }

    private async Task StartSearchingLove()
    {
        _logger.LogInformation("I started searching love of whole life!");
        InitHall();
        SearchLove();
        await Task.Delay(WaitingTime);
        _logger.LogInformation("I am done with searching love of whole life.");

        _hostApplicationLifetime.StopApplication();
    }

    private void SearchLove()
    {
        RatedContender? love = null;

        while (!_hall.IsNoContendersInHall())
        {
            RatedContender currentContender = _hall.ReturnNextContender();
            var response = _princess.CheckContender(currentContender);
            _visitedContenders.Add(currentContender);

            if (response.Contender != null)
            {
                love = response.Contender;
                break;
            }
        }

        PrintResults(_princess.CheckChoice(love));
    }

    private void PrintResults(int choiceResult)
    {
        using StreamWriter file = new("resulted.txt");
        for (int i = 0; i < _visitedContenders.Count; i++)
        {
            file.WriteLineAsync($"{i + 1}: {_visitedContenders[i].GetDetails()}");
        }

        file.WriteLineAsync("--------------------");
        file.WriteLineAsync($"Choice result: {choiceResult}");
    }

    private void InitHall()
    {
        _contendersFactory
            .CreateRatedContenders(ContenderFactory.NetGeneration, _startContendersNumber)
            .ForEach((contender) => _hall.AddNewContender(contender));
    }
}