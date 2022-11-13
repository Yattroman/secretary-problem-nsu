﻿using spn.targets.common.models.interfaces;

namespace spn.targets.common.models;

public class ChoiceStrategyByFriendHelp : IChoiceStrategy
{
    private readonly IFriend _friend;
    private RatedContender? _currentBestVariant;
    private readonly int _contendersExpected;
    private int _contendersWentThrough;
    private int SelectionBorder { get; }

    public ChoiceStrategyByFriendHelp(IFriend friend)
    {
        _friend = friend;
        _contendersWentThrough = 0;
        _currentBestVariant = null;
        _contendersExpected = 100;
        SelectionBorder = (int)(_contendersExpected / Math.E);
    }

    public StrategyResponse GetBestVariant(RatedContender? contender)
    {
        if (contender == null)
        {
            return StrategyResponse.InvalidContender();
        }

        contender.MeetWithPrincess();

        _contendersWentThrough++;

        if (_currentBestVariant == null)
        {
            _currentBestVariant = contender;
            return StrategyResponse.NotEnoughInfo();
        }

        if (_contendersWentThrough < SelectionBorder)
        {
            _currentBestVariant = GetCurrentBestContender(contender);
            return StrategyResponse.NotEnoughInfo();
        }


        if (_contendersWentThrough > _contendersExpected)
        {
            return StrategyResponse.NoVariantsSuitable();
        }

        return GetCurrentBestContender(contender) == contender
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

    private RatedContender GetCurrentBestContender(RatedContender? contender)
    {
        return _friend.GetBestContenderByComparing(_currentBestVariant, contender);
    }
}