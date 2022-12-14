using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;

namespace spn_models.targets.common.util;

public class ContenderFactory : IContendersFactory
{
    public const int SimpleGeneration = 1;
    public const int NetGeneration = 2;
    private readonly NameGenerationWithNet _nameGenerationWithNet = new();
    private readonly NameGeneratorSimple _nameGeneratorSimple = new();

    public List<RatedContender> CreateRatedContenders(int generationType, int quantity)
    {
        var contenders = new List<RatedContender>();

        var fullNames = generationType == SimpleGeneration
            ? _nameGeneratorSimple.GenerateFullNames(quantity)
            : _nameGenerationWithNet.GenerateFullNames(quantity);
        var contendersPoints = GenerateContendersPoints(quantity);

        for (var i = 0; i < quantity; i++)
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