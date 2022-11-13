using spn.targets.common.exceptions;
using spn.targets.common.models.interfaces;

namespace spn.targets.common.models;

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
        return ratedContender == null ? IChoiceStrategy.ParticularFailure :
            ratedContender.Rate < IChoiceStrategy.SuccessBorder ? IChoiceStrategy.FullFailure : ratedContender.Rate;
    }
}