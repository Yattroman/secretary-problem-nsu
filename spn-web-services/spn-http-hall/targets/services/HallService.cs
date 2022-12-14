using spn_db.targets.services;
using spn_models.targets.common.models;
using spn_models.targets.common.util;

namespace spn_http_hall.targets.services;

public class HallService : IHallService
{
    private readonly IDbIterationService _dbIterationService;
    private readonly IDbSearchLoveService _dbSearchLoveService;

    public HallService(IDbIterationService dbIterationService, IDbSearchLoveService dbSearchLoveService)
    {
        _dbIterationService = dbIterationService;
        _dbSearchLoveService = dbSearchLoveService;
    }

    public string ResetAllSearchLoveTriesStates()
    {
        _dbIterationService.DeleteAllIterations();
        return "All simulation statuses cleaned successfully!";
    }

    public Contender? ReturnNextContenderForCertainTry(int searchLoveTryId)
    {
        return _dbIterationService.GetNextContenderInSearchLoveTryIteration(searchLoveTryId);
    }

    public int ReturnSelectedContenderRate(int searchLoveTryId)
    {
        return _dbSearchLoveService.GetSelectedContenderRateInSearchLoveTry(searchLoveTryId);
    }

    public int ReturnContenderRankByName(int searchLoveTryId, string? contenderName)
    {
        var names = ContenderNamesConverter.parseFullNameFirstSecond(contenderName);

        if (names.first == null || names.second == null)
        {
            return -99;
        }

        return _dbSearchLoveService
            .GetContenderInSearchLoveTryByContenderDetails(searchLoveTryId, names.first, names.second)!.Rate;
    }
}