using spn_db.targets.entities;
using spn_models.targets.common.models;

namespace spn_db.targets.services;

public interface IDbIterationService
{
    Contender? GetNextContenderInSearchLoveTryIteration(int searchLoveTryId);
    void DeleteAllIterations();
}