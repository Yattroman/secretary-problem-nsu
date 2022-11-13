using spn.targets.task_4_db.entities;

namespace spn.targets.task_4_db.services;

public interface ISearchProcessSimulatorService
{
    void SimulateSearchProcess(int triesNumber);
    int RerunSearchProcessTryByName(string name, bool toPrint);
    public double GetAverageSearchResultValue(bool toPrint);
    List<SearchLoveTry> GetCurrentLoveSearchesStatus();
}