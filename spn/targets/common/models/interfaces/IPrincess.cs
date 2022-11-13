
namespace spn.targets.common.models.interfaces;

public interface IPrincess
{
    StrategyResponse CheckContender(RatedContender? contender);
    int CheckChoice(RatedContender? contender);
    void PrepareToNextSearch();
}