using spn_db.targets.entities;
using spn_db.targets.repositories;
using spn_models.targets.common.models;

namespace spn_db.targets.services;

public class DbIterationService : IDbIterationService
{
    private readonly IIterationRepository _iterationRepository;
    private readonly ISearchLoveRepository _searchLoveRepository;

    public DbIterationService(IIterationRepository iterationRepository, ISearchLoveRepository searchLoveRepository)
    {
        _iterationRepository = iterationRepository;
        _searchLoveRepository = searchLoveRepository;
    }
    
    public Contender? GetNextContenderInSearchLoveTryIteration(int searchLoveTryId)
    {
        var iteration = GetIterationForSearchLoveTry(searchLoveTryId);

        if (iteration == null) return null;
        
        var nextContenderOrder = iteration.CurrentContenderOrder + 1;
        var contenderEntity = _searchLoveRepository
            .GetContenderInSLTByContenderOrder(searchLoveTryId, nextContenderOrder);
        var nextContender = ReturnNextContender(contenderEntity);

        if (nextContender == null) return null;
        
        iteration.CurrentContenderOrder = nextContenderOrder;
        _iterationRepository.SaveOrUpdateIteration(iteration);

        return nextContender;
    }

    public void DeleteAllIterations()
    {
        _iterationRepository.DeleteAllIterations();
    }

    private static Contender? ReturnNextContender(ContenderEntity? contenderEntity)
    {
        return contenderEntity == null ? null : new Contender(contenderEntity.FirstName, contenderEntity.SecondName);
    }

    private IterationEntity? GetIterationForSearchLoveTry(int searchLoveTryId)
    {
        var searchLoveTry = _searchLoveRepository.GetSearchLoveTryById(searchLoveTryId);

        if (searchLoveTry == null) return null;
        
        return _iterationRepository.GetIterationBySearchLoveTry(searchLoveTryId) ?? 
               new IterationEntity(0, searchLoveTry);
    }
}