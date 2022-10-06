namespace task_1.targets;

public interface IChoiceStrategy
{
    StrategyResponse GetBestVariant(RatedContender? contender);
    int GetChoiceResult(RatedContender? contender);
}