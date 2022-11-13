using spn.targets.common.models;
using spn.targets.task_4_db.entities;
using spn.targets.task_4_db.repositories;

namespace spn.targets.task_4_db.services;

public class DbSearchLoveService : IDbSearchLoveService
{
    private readonly ISearchLoveRepository _searchLoveRepository;

    public DbSearchLoveService(ISearchLoveRepository searchLoveRepository)
    {
        _searchLoveRepository = searchLoveRepository;
    }

    public SearchLoveTry? GetSearchLoveTryByName(string name)
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

        _searchLoveRepository.SaveSearchLoveTry(new SearchLoveTry(tryName, searchResult, contenders));
    }

    public void DeleteAllSearchLoveTries()
    {
        _searchLoveRepository.DeleteAllSearchLoveTries();
    }

    public double GetAllSearchLoveTriesAverageValue()
    {
        return _searchLoveRepository.GetAllSearchLoveTriesAverageValue();
    }

    public List<SearchLoveTry> GetAllSearchLoveTries()
    {
        return _searchLoveRepository.GetAllSearchLoveTries();
    }
}