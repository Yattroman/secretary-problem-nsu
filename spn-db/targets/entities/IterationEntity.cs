namespace spn_db.targets.entities;

public class IterationEntity
{
    public int Id { get; set; }
    public int CurrentContenderOrder { get; set; }
    public int SearchLoveTryId { get; set; }
    public SearchLoveTryEntity SearchLoveTry { get; set; }

    public IterationEntity()
    {
    }

    public IterationEntity(int currentContenderOrder, SearchLoveTryEntity searchLoveTry)
    {
        CurrentContenderOrder = currentContenderOrder;
        SearchLoveTry = searchLoveTry;
    }
}