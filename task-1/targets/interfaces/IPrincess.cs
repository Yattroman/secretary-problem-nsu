namespace task_1.targets;

public interface IPrincess
{
    StrategyResponse CheckContender(RatedContender? contender);
    int CheckChoice(RatedContender? contender);
}