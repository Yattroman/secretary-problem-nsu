using spn.targets.common.models;

namespace spn_tests.targets.task_3_tests;

[TestClass]
public class StrategyByFriendTests
{
    private readonly ChoiceStrategyByFriendHelp _uut;
    private const int ContendersExpected = 100;
    private const int FirstContenderNum = 1;
    private Hall _hall;

    public StrategyByFriendTests()
    {
        _uut = new ChoiceStrategyByFriendHelp(new Friend());
        _hall = new Hall();
    }

    [TestInitialize]
    public void PrepareForTest()
    {
        _hall = new Hall();
    }

    [TestMethod]
    [DynamicData(nameof(TestInputs_Contenders), DynamicDataSourceType.Method)]
    public void ChooseContender_BestVariant_Correct(RatedContender[] preparedContenders, int bestContenderRateExpected)
    {
        foreach (var contender in preparedContenders)
        {
            _hall.AddNewContender(contender);
        }

        RatedContender? bestContender = null;

        while (!_hall.IsNoContendersInHall())
        {
            bestContender = _uut.GetBestVariant(_hall.ReturnNextContender()).Contender;
            if (bestContender != null)
            {
                break;
            }
        }

        var bestContenderRateProvided = _uut.GetChoiceResult(bestContender);
        
        Assert.AreEqual(bestContenderRateExpected, bestContenderRateProvided);
    }

    public static IEnumerable<object[]> TestInputs_Contenders()
    {
        var contendersA = new RatedContender[ContendersExpected];
        var contendersB = new RatedContender[ContendersExpected];
        var contendersC = new RatedContender[ContendersExpected];
        var contendersD = new RatedContender[ContendersExpected];

        // Rating from 1 to 100 including 
        for (var i = FirstContenderNum; i <= ContendersExpected; i++)
        {
            contendersA[i - 1] = new RatedContender(new Contender($"fn{i}", $"sn{i}"), i);
            contendersB[i - 1] = new RatedContender(new Contender($"fn{i}", $"sn{i}"), i);
            contendersC[i - 1] = new RatedContender(new Contender($"fn{i}", $"sn{i}"), i);
            contendersD[i - 1] = new RatedContender(new Contender($"fn{i}", $"sn{i}"), i);
        }

        // Swap contenders with rating 1 and 100
        TestUtil.SwapContenders(contendersA, FirstContenderNum, ContendersExpected - 1);
        // Swap contenders with rating 1 and 99
        TestUtil.SwapContenders(contendersC, FirstContenderNum, ContendersExpected - 1 - 1);
        // Swap contenders with rating 1 and 70
        TestUtil.SwapContenders(contendersD, FirstContenderNum, ContendersExpected - 1 - 30);

        return new[]
        {
            // no one suitable
            new object[] { contendersA, 10 },
            // best variant rating < ContendersExpected / 2 
            new object[] { contendersB, 0 },
            // best variant rating a bit better than contender with 99
            new object[] { contendersC, 100 },
            // best variant rating a bit better than contender with 70
            new object[] { contendersD, 71 }
        };
    }
}