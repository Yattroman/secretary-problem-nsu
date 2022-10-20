namespace task_1.targets;

public interface IContendersFactory
{
    List<RatedContender> CreateRatedContenders(int generationType, int quantity);
}