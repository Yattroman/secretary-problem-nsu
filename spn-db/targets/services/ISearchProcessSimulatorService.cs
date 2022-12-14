using spn_db.targets.entities;

namespace spn_db.targets.services;

public interface ISearchProcessSimulatorService
{
    void SimulateSearchProcess(int triesNumber);
    int FullRerunSearchProcessTryByName(string name, bool toPrint);
    public double GetAverageSearchResultValue(bool toPrint);
    List<SearchLoveTryEntity> GetCurrentLoveSearchesStatus();
}