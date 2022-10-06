namespace task_1.targets;

public interface IHall
{
    RatedContender ReturnNextContender();
    void AddNewContender(RatedContender contender);
    Boolean IsNoContendersInHall();
}