using System.Runtime.InteropServices;

namespace spn_models.targets.common.models.interfaces;

public interface IHall
{
    RatedContender? ReturnNextContender([Optional] int searchLoveTryId, [Optional] string sessionId);
    void AddNewContender(RatedContender contender);
    bool IsNoContendersInHall();
    void ClearHall([Optional] string sessionId);
}