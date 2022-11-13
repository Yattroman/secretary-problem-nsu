
namespace spn.targets.common.models.interfaces;

public interface IFriend
{
    RatedContender GetBestContenderByComparing(RatedContender? firstContender, RatedContender? secondContender);
    int GetFinalResult(RatedContender? ratedContender);
}