using spn.targets.common.models;
using spn.targets.task_4_db.entities;

namespace spn.targets.task_4_db.services;

public interface IDbSearchLoveService
{
    SearchLoveTry? GetSearchLoveTryByName(string name);
    void SaveSearchLoveTry(string tryName, List<RatedContender> ratedContenders, int searchResult);
    void DeleteAllSearchLoveTries();
    double GetAllSearchLoveTriesAverageValue();
    public List<SearchLoveTry> GetAllSearchLoveTries();
}