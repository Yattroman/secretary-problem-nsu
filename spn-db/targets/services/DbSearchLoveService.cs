using spn_db.targets.entities;
using spn_db.targets.repositories;
using spn_models.targets.common.models;

namespace spn_db.targets.services;

public class DbSearchLoveService : IDbSearchLoveService
{
    private readonly ISearchLoveRepository _searchLoveRepository;

    public DbSearchLoveService(ISearchLoveRepository searchLoveRepository)
    {
        _searchLoveRepository = searchLoveRepository;
    }

    public SearchLoveTryEntity? GetSearchLoveTryByName(string name)
    {
        return _searchLoveRepository.GetSearchLoveTryByName(name);
    }

    public void SaveSearchLoveTry(string tryName, List<RatedContender> ratedContenders, int searchResult)
    {
        var contenders = ratedContenders
            .Select(ratedContender =>
                new ContenderEntity(ratedContender.GetFirstName(), ratedContender.GetSecondName(),
                    ratedContender.OrderNumber, ratedContender.Rate))
            .ToList();

        _searchLoveRepository.SaveSearchLoveTry(new SearchLoveTryEntity(tryName, searchResult, contenders));
    }

    public void DeleteAllSearchLoveTries()
    {
        _searchLoveRepository.DeleteAllSearchLoveTries();
    }

    public double GetAllSearchLoveTriesAverageValue()
    {
        return _searchLoveRepository.GetAllSearchLoveTriesAverageValue();
    }

    public List<SearchLoveTryEntity> GetAllSearchLoveTries()
    {
        return _searchLoveRepository.GetAllSearchLoveTries();
    }

    public int GetSelectedContenderRateInSearchLoveTry(int searchLoveTryId)
    {
        return _searchLoveRepository.GetSelectedContenderRateInSearchLoveTry(searchLoveTryId);
    }

    public RatedContender? GetContenderInSearchLoveTryByContenderDetails(int searchLoveTryId, string firstName,
        string secondName)
    {
        var contenderEntity = _searchLoveRepository
            .GetRatedContenderInSLTByContenderDetails(searchLoveTryId, firstName, secondName);
        return ReturnRatedContender(contenderEntity);
    }

    private static RatedContender? ReturnRatedContender(ContenderEntity? contenderEntity)
    {
        return contenderEntity == null
            ? null
            : new RatedContender(
                contenderEntity.FirstName,
                contenderEntity.SecondName,
                contenderEntity.Rating
            );
    }
}