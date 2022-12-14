using System.Runtime.InteropServices;
using spn_models.targets.common.exceptions;
using spn_models.targets.common.models.interfaces;

namespace spn_models.targets.common.models;

public class Hall : IHall
{
    private readonly Queue<RatedContender> _contenders = new();

    public RatedContender? ReturnNextContender([Optional] int searchLoveTryId, [Optional] string sessionId)
    {
        if (IsNoContendersInHall()) throw new SecretaryProblemException(ErrorType.HallIsEmpty());

        return _contenders.Dequeue();
    }

    public void AddNewContender(RatedContender contender)
    {
        _contenders.Enqueue(contender);
    }

    public void ClearHall([Optional] string sessionId)
    {
        _contenders.Clear();
    }

    public bool IsNoContendersInHall()
    {
        return _contenders.Count == 0;
    }
}