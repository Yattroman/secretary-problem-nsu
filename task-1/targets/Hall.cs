namespace task_1.targets;

public class Hall : IHall
{
    private Queue<RatedContender> _contenders = new Queue<RatedContender>();

    public RatedContender ReturnNextContender()
    {
        return _contenders.Dequeue();
    }

    public void AddNewContender(RatedContender contender)
    {
        _contenders.Enqueue(contender);
    }

    public bool IsNoContendersInHall()
    {
        return _contenders.Count == 0;
    }
}