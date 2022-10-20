using task_1.targets.common.exceptions;

namespace task_1.targets;

public class Hall : IHall
{
    private readonly Queue<RatedContender> _contenders = new Queue<RatedContender>();

    public RatedContender ReturnNextContender()
    {
        if (IsNoContendersInHall())
        {
            throw new SecretaryProblemException(ErrorType.HallIsEmpty());
        }

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