using spn_http_princess.targets.clients;
using spn_http_princess.targets.dto;
using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;

namespace spn_http_princess.targets.services;

public class PrincessService : IPrincessService
{
    private readonly ISimulatedHall _hall;
    private readonly IPrincess _princess;

    private readonly List<RatedContender> _visitedContenders = new List<RatedContender>();

    public PrincessService(ISimulatedHall simulatedHall, IPrincess princess)
    {
        _hall = simulatedHall;
        _princess = princess;
    }

    public void Test()
    {
        _hall.ClearHall(
            "2343"
        );
    }

    public SearchLoveTryResponse SearchLoveTryByPreparedData(int searchLoveTryId, string sessionId)
    {
        _hall.ClearHall(sessionId);

        RatedContender? love = null;
        var orderCounter = 1;

        var currentContender = _hall.ReturnNextContender(searchLoveTryId, sessionId);
        do
        {
            var strategyResponse = _princess.CheckContender(currentContender, searchLoveTryId, sessionId);
            currentContender!.OrderNumber = orderCounter++;
            _visitedContenders.Add(currentContender);

            if (strategyResponse.Contender != null)
            {
                love = strategyResponse.Contender;
                break;
            }

            currentContender = _hall.ReturnNextContender(searchLoveTryId, sessionId);
        } while (currentContender != null);

        var expectedSelectedContenderRank = _hall.GetSelectedContenderRank(searchLoveTryId, sessionId);
        var actualSelectedContenderRank = _hall.ShowRealContenderRank(searchLoveTryId, sessionId,
            currentContender?.GetDetailsBasic());

        return new SearchLoveTryResponse(
            expectedSelectedContenderRank,
            actualSelectedContenderRank,
            love?.GetDetails()
        );
    }
}