using spn_models.targets.common.models;

namespace spn_http_hall.targets.services;

public interface IHallService
{
    string ResetAllSearchLoveTriesStates();
    Contender? ReturnNextContenderForCertainTry(int searchLoveTryId);
    int ReturnSelectedContenderRate(int searchLoveTryId);
    int ReturnContenderRankByName(int searchLoveTryId, string? contenderName);
}