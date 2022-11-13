using spn.targets.common.models.interfaces;

namespace spn.targets.common.models;

public class Princess : IPrincess
{
    private IChoiceStrategy _strategy;

    public Princess(IChoiceStrategy strategy)
    {
        _strategy = strategy;
    }

    public StrategyResponse CheckContender(RatedContender? contender)
    {
        return _strategy.GetBestVariant(contender);
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