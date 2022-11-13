using spn.targets.common.exceptions;
using spn.targets.common.models;

namespace spn_tests.targets.task_3_tests;

[TestClass]
public class HallTests
{
    private readonly Hall _uut;

    private const int FirstContenderNum = 0;

    public HallTests()
    {
        _uut = new Hall();
    }

    [TestMethod]
    public void CallContender_EmptyHall_GetException()
    {
        var (contenderA, contenderB) = TestUtil.CreateTwoContenders(20, 80);
        _uut.AddNewContender(contenderA);
        _uut.AddNewContender(contenderB);
        _uut.ReturnNextContender();
        _uut.ReturnNextContender();

        var providedException = Assert.ThrowsException<SecretaryProblemException>(() => _uut.ReturnNextContender());
        Assert.AreEqual(providedException.Message, 
            TestUtil.SecretaryProblemExceptionMessage(ErrorType.HallIsEmpty()));
    }
    
    [TestMethod]
    [DataRow(1)]
    [DataRow(10)]
    [DataRow(50)]
    [DataRow(101)]
    public void CallContenders_HallWithContenders_Correct(int contendersNumber)
    {
        var preparedContenders = new List<RatedContender>();

        for (var i = FirstContenderNum; i < contendersNumber; i++)
        {
            var contender = new RatedContender(new Contender($"fn{i}", $"sn{i}"), i);
            preparedContenders.Add(contender);
            _uut.AddNewContender(contender);
        }

        for (var i = FirstContenderNum; i < contendersNumber; i++)
        {
            if (_uut.IsNoContendersInHall()) continue;
            var nextContender = _uut.ReturnNextContender();
            Assert.AreEqual(nextContender, preparedContenders[i]);
        }
    }
    
}