using task_1.targets;

namespace spn_tests.targets.task_3;

[TestClass]
public class ContenderFactoryTests
{
    private ContenderFactory _uut;
    private const int ContendersExpected = 100;

    public ContenderFactoryTests()
    {
        _uut = new ContenderFactory();
    }

    [TestMethod]
    public void GenerateContenders_Random_UniqueResults()
    {
        var contenders = _uut.CreateRatedContenders(ContenderFactory.NetGeneration, ContendersExpected);
        var contendersSet = contenders.Select(contender => (contender.GetFirstName(), contender.GetSecondName()));
        
        Assert.AreEqual(ContendersExpected, contendersSet.Count());
    }
    
}