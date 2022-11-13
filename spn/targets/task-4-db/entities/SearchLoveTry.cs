namespace spn.targets.task_4_db.entities;

public class SearchLoveTry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SearchResult { get; set; }

    public List<ContenderEntity> Contenders { get; set; }

    public SearchLoveTry()
    {
    }

    public SearchLoveTry(string name, int searchResult, List<ContenderEntity> contenders)
    {
        Name = name;
        SearchResult = searchResult;
        Contenders = contenders;
    }
}