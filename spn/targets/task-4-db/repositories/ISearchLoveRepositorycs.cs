using spn.targets.task_4_db.entities;

namespace spn.targets.task_4_db.repositories;

public interface ISearchLoveRepository
{
    SearchLoveTry? GetSearchLoveTryByName(string name);
    void SaveSearchLoveTry(SearchLoveTry searchLoveTry);
    double GetAllSearchLoveTriesAverageValue();
    void DeleteAllSearchLoveTries();
    public List<SearchLoveTry> GetAllSearchLoveTries();
}