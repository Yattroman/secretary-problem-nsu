using spn_db.targets.db_conf;
using spn_db.targets.entities;

namespace spn_db.targets.repositories;

public class IterationRepository : IIterationRepository
{
    private readonly EnvironmentContext _context;

    public IterationRepository(EnvironmentContext context)
    {
        _context = context;
    }

    public IterationEntity? GetIterationBySearchLoveTry(int searchLoveTryId)
    {
        return _context.IterationEntities
            .FirstOrDefault(iteration => iteration.SearchLoveTry.Id == searchLoveTryId);
    }

    public void DeleteAllIterations()
    {
        foreach (var id in _context.IterationEntities.Select(e => e.Id))
        {
            var iterationEntity = new IterationEntity { Id = id };
            _context.IterationEntities.Attach(iterationEntity);
            _context.IterationEntities.Remove(iterationEntity);
        }

        _context.SaveChanges();
    }

    public void SaveOrUpdateIteration(IterationEntity iteration)
    {
        var existedIteration = _context.IterationEntities
            .FirstOrDefault(x => x.Id == iteration.Id);
        
        if (existedIteration == null)
        {
            _context.IterationEntities
                .Add(iteration);
        }
        else
        {
            iteration.Id = existedIteration.Id;
            _context.Entry(existedIteration).CurrentValues
                .SetValues(iteration);
        }

        _context.SaveChanges();
    }

}