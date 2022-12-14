using spn_http_princess.targets.dto;

namespace spn_http_princess.targets.services;

public interface IPrincessService
{
    SearchLoveTryResponse SearchLoveTryByPreparedData(int searchLoveTryId, string sessionId);
}