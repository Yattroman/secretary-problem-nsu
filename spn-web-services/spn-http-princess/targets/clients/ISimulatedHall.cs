using System.Runtime.InteropServices;
using spn_models.targets.common.models.interfaces;

namespace spn_http_princess.targets.clients;

public interface ISimulatedHall : IHall
{
    int GetSelectedContenderRank([Optional] int searchLoveTryId, [Optional] string sessionId);
    int ShowRealContenderRank([Optional] int searchLoveTryId, [Optional] string sessionId, string? contenderName);
}