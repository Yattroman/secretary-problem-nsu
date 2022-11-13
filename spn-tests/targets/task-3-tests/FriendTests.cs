using spn.targets.common.exceptions;
using spn.targets.common.models;

namespace spn_tests.targets.task_3_tests;

[TestClass]
public class FriendTests
{
    private readonly Friend _uut;

    public FriendTests()
    {
        _uut = new Friend();
    }

    [TestMethod]
    [DataRow(20, 80)]
    [DataRow(80, 20)]
    [DataRow(49, 51)]
    [DataRow(51, 49)]
    public void Compare_TwoContenders_Correct(int ratingA, int ratingB)
    {
        var (contenderA, contenderB) = TestUtil.CreateTwoContenders(ratingA, ratingB);
        var requiredBest = contenderA.Rate > contenderB.Rate ? contenderA : contenderB;
        contenderA.MeetWithPrincess();
        contenderB.MeetWithPrincess();

        var providedBest = _uut.GetBestContenderByComparing(contenderA, contenderB);

        Assert.AreEqual(requiredBest, providedBest);
    }

    [TestMethod]
    [DataRow(true, false)]
    [DataRow(false, true)]
    [DataRow(false, false)]
    public void CallContenders_Unfamiliar_Exception
        (bool isContenderAFamiliarWithPrincess, bool isContenderBFamiliarWithPrincess)
    {
        var (contenderA, contenderB) = TestUtil.CreateTwoContenders(20, 80);
        if(isContenderAFamiliarWithPrincess) {contenderA.MeetWithPrincess();}
        if(isContenderBFamiliarWithPrincess) {contenderB.MeetWithPrincess();}
        
        var providedException = Assert.ThrowsException<SecretaryProblemException>(
            () => _uut.GetBestContenderByComparing(contenderA, contenderB)
        );
        
        Assert.AreEqual(providedException.Message, 
            TestUtil.SecretaryProblemExceptionMessage(ErrorType.UnfamiliarContender()));
    }
}