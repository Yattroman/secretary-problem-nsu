namespace task_1.targets;

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
}