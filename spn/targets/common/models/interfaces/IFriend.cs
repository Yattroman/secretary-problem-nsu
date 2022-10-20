namespace task_1.targets;

public interface IFriend
{
    RatedContender GetBestContenderByComparing(RatedContender? firstContender, RatedContender? secondContender);
    int GetFinalResult(RatedContender? ratedContender);
}