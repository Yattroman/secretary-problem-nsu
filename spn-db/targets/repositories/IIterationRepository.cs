using spn_db.targets.entities;
using spn_models.targets.common.models;

namespace spn_db.targets.repositories;

public interface IIterationRepository
{
    IterationEntity? GetIterationBySearchLoveTry(int searchLoveTryId);
    void DeleteAllIterations();
    void SaveOrUpdateIteration(IterationEntity iterationEntity);
}