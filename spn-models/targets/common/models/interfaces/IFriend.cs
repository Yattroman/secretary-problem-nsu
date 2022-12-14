using System.Runtime.InteropServices;

namespace spn_models.targets.common.models.interfaces;

public interface IFriend
{
    RatedContender GetBestContenderByComparing(RatedContender? firstContender, RatedContender? secondContender,
        [Optional] int searchLoveTryId, [Optional] string sessionId);

    int GetFinalResult(RatedContender? ratedContender);
}