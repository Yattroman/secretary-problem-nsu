using System.Runtime.InteropServices;

namespace spn_models.targets.common.models.interfaces;

public interface IChoiceStrategy
{
    const int FullSuccess = 100;
    const int ParticularFailure = 10;
    const int FullFailure = 0;
    const int SuccessBorder = 50;

    StrategyResponse GetBestVariant(RatedContender? contender, [Optional] int searchLoveTryId,
        [Optional] string sessionId);
    int GetChoiceResult(RatedContender? contender);
    void CleanupStrategy();
}