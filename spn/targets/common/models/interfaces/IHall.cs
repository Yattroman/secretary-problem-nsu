
namespace spn.targets.common.models.interfaces;

public interface IHall
{
    RatedContender ReturnNextContender();
    void AddNewContender(RatedContender contender);
    Boolean IsNoContendersInHall();
    void ClearHall();
}