

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using spn_db.targets.db_conf;
using spn_db.targets.repositories;
using spn_db.targets.services;
using spn_models.targets.common.models;
using spn_models.targets.common.util;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace spn_tests.targets.task_4_tests;

[TestFixture]
public class SimulatorServiceTest
{
    private static readonly DbContextOptions<EnvironmentContext> ContextOptions =
        new DbContextOptionsBuilder<EnvironmentContext>()
            .UseInMemoryDatabase(databaseName: "SPN")
            .Options;

    private readonly EnvironmentContext _context;
    private readonly ISearchProcessSimulatorService _uut;
    private const int LoveSearchesNumber = 25;
    private const string ConfigAppSettingsPath = "targets/resources/appsettings.json";

    public SimulatorServiceTest()
    {
        _context = new EnvironmentContext(ContextOptions);
        _uut = new SearchProcessSimulatorService(
            new DbSearchLoveService(new SearchLoveRepository(_context)),
            new Hall(),
            new Princess(new ChoiceStrategyByFriendHelp(new Friend())),
            new Logger<SearchProcessSimulatorService>(new LoggerFactory()),
            new ConfigurationBuilder().AddJsonFile(ConfigAppSettingsPath).Build(),
            new ContenderFactory()
        );
    }

    [Test, Order(1)]
    public void SimulateNSearches_SearchesSimulated_SavedInDb()
    {
        var searchesBefore = _uut.GetCurrentLoveSearchesStatus();
        
        _uut.SimulateSearchProcess(LoveSearchesNumber);
        var searchesAfter = _uut.GetCurrentLoveSearchesStatus();

        Assert.AreEqual(0, searchesBefore.Count);
        Assert.AreEqual(LoveSearchesNumber, searchesAfter.Count);
    }

    [Test, Order(2)]
    public void GetSearchesByNameFromDb_CheckEqualityWithManualSearches_AllAreEqual()
    {
        var searches = _uut.GetCurrentLoveSearchesStatus();
        var resultsList = 
            (from search 
                in searches 
                let searchResult = _uut.FullRerunSearchProcessTryByName(search.Name, false) 
                select (search.SearchResult, searchResult))
            .ToList();

        foreach (var result in resultsList)
        {
            Assert.AreEqual(result.Item1, result.Item2);
        }
    }
    
    [Test, Order(3)]
    public void GetSearchesAverageValue_CheckEqualityWithManualAverageCalculation_AllAreEqual()
    {
        var searches = _uut.GetCurrentLoveSearchesStatus();
        var manualAverage = (from search in searches select search.SearchResult).Average();
        var serviceAverage = _uut.GetAverageSearchResultValue(false);
        
        Assert.AreEqual(manualAverage, serviceAverage);
    }

    [ClassCleanup]
    public void CleanContext()
    {
        _context.Database.EnsureDeleted();
    }
}