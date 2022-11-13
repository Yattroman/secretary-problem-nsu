using spn.targets.common.models;
using spn.targets.common.models.interfaces;

namespace spn.targets.common.util;

public class ContenderFactory : IContendersFactory
{
    private readonly NameGeneratorSimple _nameGeneratorSimple = new NameGeneratorSimple();
    private readonly NameGenerationWithNet _nameGenerationWithNet = new NameGenerationWithNet();
    public const int SimpleGeneration = 1;
    public const int NetGeneration = 2;

    public List<RatedContender> CreateRatedContenders(int generationType, int quantity)
    {
        var contenders = new List<RatedContender>();

        var fullNames = generationType == SimpleGeneration
            ? _nameGeneratorSimple.GenerateFullNames(quantity)
            : _nameGenerationWithNet.GenerateFullNames(quantity);
        var contendersPoints = GenerateContendersPoints(quantity);

        for (int i = 0; i < quantity; i++)
        {
            var (fullname, randomPoint) = (fullNames[i], contendersPoints[i]);
            contenders.Add
                (new RatedContender(new Contender(fullname.First, fullname.Second), randomPoint));
        }

        return contenders;
    }

    private static int[] GenerateContendersPoints(int quantity)
    {
        var rnd = new Random();
        return Enumerable.Range(1, quantity).OrderBy(c => rnd.Next()).ToArray();
    }
}