using System.Runtime.InteropServices;
using spn_models.targets.common.models.interfaces;

namespace spn_models.targets.common.models;

public class Princess : IPrincess
{
    private readonly IChoiceStrategy _strategy;

    public Princess(IChoiceStrategy strategy)
    {
        _strategy = strategy;
    }

    public StrategyResponse CheckContender(RatedContender? contender, [Optional] int searchLoveTryId,
        [Optional] string sessionId)
    {
        return _strategy.GetBestVariant(contender, searchLoveTryId, sessionId);
    }

    public int CheckChoice(RatedContender? contender)
    {
        return _strategy.GetChoiceResult(contender);
    }

    public void PrepareToNextSearch()
    {
        _strategy.CleanupStrategy();
    }
}