using System.Runtime.InteropServices;
using spn_models.targets.common.models.interfaces;

namespace spn_models.targets.common.models;

public class ChoiceStrategyByFriendHelp : IChoiceStrategy
{
    private readonly int _contendersExpected;
    private readonly IFriend _friend;
    private int _contendersWentThrough;
    private RatedContender? _currentBestVariant;

    public ChoiceStrategyByFriendHelp(IFriend friend)
    {
        _friend = friend;
        _contendersWentThrough = 0;
        _currentBestVariant = null;
        _contendersExpected = 100;
        SelectionBorder = (int)(_contendersExpected / Math.E);
    }

    private int SelectionBorder { get; }

    public StrategyResponse GetBestVariant(RatedContender? contender, [Optional] int searchLoveTryId,
        [Optional] string sessionId)
    {
        if (contender == null) return StrategyResponse.InvalidContender();

        contender.MeetWithPrincess();

        _contendersWentThrough++;

        if (_currentBestVariant == null)
        {
            _currentBestVariant = contender;
            return StrategyResponse.NotEnoughInfo();
        }

        if (_contendersWentThrough < SelectionBorder)
        {
            _currentBestVariant = GetCurrentBestContender(contender, searchLoveTryId, sessionId);
            return StrategyResponse.NotEnoughInfo();
        }

        if (_contendersWentThrough > _contendersExpected) return StrategyResponse.NoVariantsSuitable();

        return GetCurrentBestContender(contender, searchLoveTryId, sessionId).GetDetailsBasic() ==
               contender.GetDetailsBasic()
            ? StrategyResponse.ThatIsBestChoice(contender)
            : StrategyResponse.ItCanBeBetter();
    }

    public void CleanupStrategy()
    {
        _currentBestVariant = null;
        _contendersWentThrough = 0;
    }

    public int GetChoiceResult(RatedContender? contender)
    {
        return _friend.GetFinalResult(contender);
    }

    private RatedContender GetCurrentBestContender(RatedContender? contender, [Optional] int searchLoveTryId,
        [Optional] string sessionId)
    {
        return _friend.GetBestContenderByComparing(_currentBestVariant, contender, 
            searchLoveTryId, sessionId);
    }
}