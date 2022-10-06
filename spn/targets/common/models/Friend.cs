namespace task_1.targets;

public class Friend : IFriend
{
    public RatedContender GetBestContendersByComparing(RatedContender firstContender, RatedContender secondContender)
    {
        return firstContender.Rate > secondContender.Rate ? firstContender : secondContender;
    }
}