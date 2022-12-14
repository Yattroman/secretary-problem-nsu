using spn_db.targets.services;
using spn.targets.task_2_di_containers;

namespace spn.targets.task_4_db;

public class SearchProcHostedWithDb : BackgroundService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<SearchProcHosted> _logger;
    private readonly ISearchProcessSimulatorService _searchProcessSimulatorService;

    private readonly UsingType _usingType;
    private readonly int _triesNumber;

    public SearchProcHostedWithDb(IHostApplicationLifetime hostApplicationLifetime, ILogger<SearchProcHosted> logger,
        ISearchProcessSimulatorService searchProcessSimulatorService, IConfiguration configuration)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _searchProcessSimulatorService = searchProcessSimulatorService;

        _usingType = configuration.GetValue<UsingType>("SearchLoveParams:UsingType");
        _triesNumber = configuration.GetValue<int>("SearchLoveParams:SearchLoveTries");
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

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var task = _usingType switch
        {
            UsingType.UpdateBase => SearchLoveMultipleTimes(),
            UsingType.RecallSearches => RecallSearchesLoves(),
            UsingType.AverageValue => PrintAverageLoveSearchesValue(),
            _ => InvalidUsingTypeLog()
        };

        return Task.CompletedTask;
    }

    private Task InvalidUsingTypeLog()
    {
        _logger.LogInformation("Given Invalid Using Type!");
        return Task.CompletedTask;
    }

    private Task RecallSearchesLoves()
    {
        var searches = Environment.GetCommandLineArgs().Skip(1);

        foreach (var search in searches)
        {
            _logger.LogInformation($"I'm recalling search: {search}!");
            var searchResult = _searchProcessSimulatorService.FullRerunSearchProcessTryByName(search, true);
            _logger.LogInformation($"Search result: {searchResult}!");
        }

        _logger.LogInformation("I am done with recalling searches.");
        _hostApplicationLifetime.StopApplication();

        return Task.CompletedTask;
    }

    private Task SearchLoveMultipleTimes()
    {
        _logger.LogInformation("I started searching love of whole life!");
        _searchProcessSimulatorService.SimulateSearchProcess(_triesNumber);
        _logger.LogInformation("I am done with searching love of whole life.");
        _hostApplicationLifetime.StopApplication();

        return Task.CompletedTask;
    }

    private Task PrintAverageLoveSearchesValue()
    {
        _searchProcessSimulatorService.GetAverageSearchResultValue(true);
        _hostApplicationLifetime.StopApplication();

        return Task.CompletedTask;
    }
}