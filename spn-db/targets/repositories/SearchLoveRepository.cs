using System.Data.Entity;
using spn_db.targets.db_conf;
using spn_db.targets.entities;
using spn_models.targets.common.models;

namespace spn_db.targets.repositories;

public class SearchLoveRepository : ISearchLoveRepository
{
    private readonly EnvironmentContext _context;

    public SearchLoveRepository(EnvironmentContext context)
    {
        _context = context;
    }

    public SearchLoveTryEntity? GetSearchLoveTryByName(string name)
    {
        var searchLoveTry = _context.SearchLoveTries
            .FirstOrDefault(searchLoveTry => searchLoveTry.Name == name);
        _context.Entry(searchLoveTry)
            .Collection(search => search.Contenders)
            .Load();

        return searchLoveTry;
    }

    public SearchLoveTryEntity? GetSearchLoveTryById(int searchLoveTryId)
    {
        var searchLoveTry = _context.SearchLoveTries
            .FirstOrDefault(searchLoveTry => searchLoveTry.Id == searchLoveTryId);
        _context.Entry(searchLoveTry)
            .Collection(search => search.Contenders)
            .Load();

        return searchLoveTry;
    }

    public void SaveSearchLoveTry(SearchLoveTryEntity searchLoveTry)
    {
        _context.SearchLoveTries
            .Add(searchLoveTry);

        ConfirmSave();
    }

    public double GetAllSearchLoveTriesAverageValue()
    {
        return _context.SearchLoveTries
            .Average(search => search.SearchResult);
    }

    public List<SearchLoveTryEntity> GetAllSearchLoveTries()
    {
        return _context.SearchLoveTries
            .Include(search => search.Contenders)
            .ToList();
    }

    public int GetSelectedContenderRateInSearchLoveTry(int searchLoveTryId)
    {
        return _context.SearchLoveTries
            .FirstOrDefault(searchLoveTry => searchLoveTry.Id == searchLoveTryId)?.SearchResult ?? -1;
    }

    public ContenderEntity? GetContenderInSLTByContenderOrder(int searchLoveTryId, int contenderOrderNumber)
    {
        var searchLoveTry = GetSearchLoveTryById(searchLoveTryId);
        return searchLoveTry?.Contenders
            .FirstOrDefault(contender => contender.OrderNumber == contenderOrderNumber);
    }

    public ContenderEntity? GetRatedContenderInSLTByContenderDetails(int searchLoveTryId, string contenderFirstName,
        string contenderSecondName)
    {
        var searchLoveTry = GetSearchLoveTryById(searchLoveTryId);
        return searchLoveTry?.Contenders
            .FirstOrDefault(contender =>
                contender.FirstName == contenderFirstName && contender.SecondName == contenderSecondName);
    }

    public void DeleteAllSearchLoveTries()
    {
        foreach (var id in _context.SearchLoveTries.Select(e => e.Id))
        {
            var searchLoveTry = new SearchLoveTryEntity { Id = id };
            _context.SearchLoveTries.Attach(searchLoveTry);
            _context.SearchLoveTries.Remove(searchLoveTry);
        }

        ConfirmSave();
    }

    private void ConfirmSave()
    {
        _context.SaveChanges();
    }
}