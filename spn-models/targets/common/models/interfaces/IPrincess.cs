using System.Runtime.InteropServices;

namespace spn_models.targets.common.models.interfaces;

public interface IPrincess
{
    StrategyResponse CheckContender(RatedContender? contender, [Optional] int searchLoveTryId,
        [Optional] string sessionId);

    int CheckChoice(RatedContender? contender);
    void PrepareToNextSearch();
}