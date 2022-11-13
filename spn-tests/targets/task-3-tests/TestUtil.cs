using spn.targets.common.exceptions;
using spn.targets.common.models;

namespace spn_tests.targets.task_3_tests;

public static class TestUtil
{
    public static (RatedContender, RatedContender) CreateTwoContenders(int ratingA, int ratingB)
    {
        return (new RatedContender(new Contender("fnA", "snA"), ratingA),
                new RatedContender(new Contender("fnA", "snA"), ratingB));
    }

    public static string SecretaryProblemExceptionMessage(ErrorType errorType)
    {
        return $"{errorType.ErrorCode}: {errorType.Message}";
    }
    
    public static void SwapContenders(RatedContender[] contenders, int contenderAIdx, int contenderBIdx)
    {
        (contenders[contenderBIdx], contenders[contenderAIdx]) = (contenders[contenderAIdx], contenders[contenderBIdx]);
    }
    
}