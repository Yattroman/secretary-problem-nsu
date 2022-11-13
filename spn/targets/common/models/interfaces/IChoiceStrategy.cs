
namespace spn.targets.common.models.interfaces;

public interface IChoiceStrategy
{
    const int FullSuccess = 100;
    const int ParticularFailure = 10;
    const int FullFailure = 0;
    const int SuccessBorder = 50;

    StrategyResponse GetBestVariant(RatedContender? contender);
    int GetChoiceResult(RatedContender? contender);
    void CleanupStrategy();
}