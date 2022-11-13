using System.Data.Entity;
using spn.targets.task_4_db.db_conf;
using spn.targets.task_4_db.entities;

namespace spn.targets.task_4_db.repositories;

public class SearchLoveRepository : ISearchLoveRepository
{
    private readonly EnvironmentContext _context;

    public SearchLoveRepository(EnvironmentContext context)
    {
        _context = context;
    }

    public SearchLoveTry? GetSearchLoveTryByName(string name)
    {
        var searchLoveTry = _context.SearchLoveTries.FirstOrDefault(searchLoveTry => searchLoveTry.Name == name);
        _context.Entry(searchLoveTry)
            .Collection(search => search.Contenders)
            .Load();

        return searchLoveTry;
    }

    public void SaveSearchLoveTry(SearchLoveTry searchLoveTry)
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

    public List<SearchLoveTry> GetAllSearchLoveTries()
    {
        return _context.SearchLoveTries
            .Include(search => search.Contenders)
            .ToList();
    }

    public void DeleteAllSearchLoveTries()
    {
        foreach (var id in _context.SearchLoveTries.Select(e => e.Id))
        {
            var searchLoveTry = new SearchLoveTry { Id = id };
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