using spn_db.targets.entities;

namespace spn_db.targets.repositories;

public interface ISearchLoveRepository
{
    SearchLoveTryEntity? GetSearchLoveTryByName(string name);
    void SaveSearchLoveTry(SearchLoveTryEntity searchLoveTry);
    double GetAllSearchLoveTriesAverageValue();
    void DeleteAllSearchLoveTries();
    List<SearchLoveTryEntity> GetAllSearchLoveTries();
    int GetSelectedContenderRateInSearchLoveTry(int searchLoveTryId);
    ContenderEntity? GetContenderInSLTByContenderOrder(int searchLoveTryId, int contenderOrderNumber);
    ContenderEntity? GetRatedContenderInSLTByContenderDetails(int searchLoveTryId, string contenderFirstName,
        string contenderSecondName);

    SearchLoveTryEntity? GetSearchLoveTryById(int searchLoveTryId);
}