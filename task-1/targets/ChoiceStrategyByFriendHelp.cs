namespace task_1.targets;

public class ChoiceStrategyByFriendHelp : IChoiceStrategy
{
    private readonly IFriend _friend;
    private RatedContender? _currentBestVariant;
    private readonly int _contendersExpected;
    private readonly int _selectionBorder;
    private int _contendersWentThrough;

    public ChoiceStrategyByFriendHelp(IFriend friend, int contendersExpected)
    {
        _friend = friend;
        _contendersWentThrough = 0;
        _currentBestVariant = null;
        _contendersExpected = contendersExpected;
        _selectionBorder = (int)(contendersExpected / Math.E);
    }

    public StrategyResponse GetBestVariant(RatedContender? contender)
    {
        if (contender == null)
        {
            return StrategyResponse.InvalidContender();
        }

        _contendersWentThrough++;

        if (_currentBestVariant == null)
        {
            _currentBestVariant = contender;
            return StrategyResponse.NotEnoughInfo();
        }

        if (_contendersWentThrough < _selectionBorder)
        {
            _currentBestVariant = GetCurrentBestContender(contender);
            return StrategyResponse.NotEnoughInfo();
        }

        if (_contendersWentThrough == _contendersExpected)
        {
            return StrategyResponse.NoVariantsSuitable();
        }

        return GetCurrentBestContender(contender) == contender
            ? StrategyResponse.ThatIsBestChoice(contender)
            : StrategyResponse.ItCanBeBetter();
    }

    public int GetChoiceResult(RatedContender? contender)
    {
        return contender == null ? 10 : contender.Rate < 50 ? 0 : contender.Rate;
    }

    private RatedContender GetCurrentBestContender(RatedContender? contender)
    {
        return contender == null
            ? _currentBestVariant
            : _friend.GetBestContendersByComparing(_currentBestVariant, contender);
    }
}