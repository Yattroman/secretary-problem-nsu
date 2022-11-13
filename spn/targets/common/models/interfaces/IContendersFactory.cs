
namespace spn.targets.common.models.interfaces;

public interface IContendersFactory
{
    List<RatedContender> CreateRatedContenders(int generationType, int quantity);
}