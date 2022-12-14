using spn_db.targets.entities;
using spn_models.targets.common.models;

namespace spn_db.targets.services;

public interface IDbSearchLoveService
{
    SearchLoveTryEntity? GetSearchLoveTryByName(string name);
    void SaveSearchLoveTry(string tryName, List<RatedContender> ratedContenders, int searchResult);
    void DeleteAllSearchLoveTries();
    double GetAllSearchLoveTriesAverageValue();
    List<SearchLoveTryEntity> GetAllSearchLoveTries();
    int GetSelectedContenderRateInSearchLoveTry(int searchLoveTryId);
    RatedContender? GetContenderInSearchLoveTryByContenderDetails(int searchLoveTryId, string firstName,
        string secondName);
}