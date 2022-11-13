using spn.targets.common.exceptions;
using spn.targets.common.models.interfaces;

namespace spn.targets.common.models;

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

    public void ClearHall()
    {
        _contenders.Clear();
    }

    public bool IsNoContendersInHall()
    {
        return _contenders.Count == 0;
    }
}