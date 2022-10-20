using task_1.targets.common.exceptions;

namespace task_1.targets;

public class Friend : IFriend
{
    public RatedContender GetBestContenderByComparing(RatedContender? firstContender, RatedContender? secondContender)
    {
        if (firstContender == null || secondContender == null)
        {
            throw new SecretaryProblemException(ErrorType.InvalidContender());
        }

        if (!firstContender.IsFamiliarWithPrincess || !secondContender.IsFamiliarWithPrincess)
        {
            throw new SecretaryProblemException(ErrorType.UnfamiliarContender());
        }

        return firstContender.Rate > secondContender.Rate ? firstContender : secondContender;
    }

    public int GetFinalResult(RatedContender? ratedContender)
    {
        return ratedContender == null ? 10 : ratedContender.Rate < 50 ? 0 : ratedContender.Rate;
    }
}