namespace spn_db.targets.entities;

public class SearchLoveTryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SearchResult { get; set; }

    public List<ContenderEntity> Contenders { get; set; }

    public SearchLoveTryEntity() 
    {
    }

    public SearchLoveTryEntity(string name, int searchResult, List<ContenderEntity> contenders)
    {
        Name = name;
        SearchResult = searchResult;
        Contenders = contenders;
    }
}