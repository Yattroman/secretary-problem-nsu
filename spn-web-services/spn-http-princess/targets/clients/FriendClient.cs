using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using spn_models.targets.common.dto;
using spn_models.targets.common.exceptions;
using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;
using spn_models.targets.common.util;

namespace spn_http_princess.targets.clients;

public class FriendClient : IFriend
{
    private readonly HttpClient _httpClient;

    public FriendClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5026");
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public RatedContender GetBestContenderByComparing(RatedContender? firstContender, RatedContender? secondContender,
        [Optional] int searchLoveTryId, [Optional] string sessionId)
    {
        var webResponse = _httpClient.PostAsJsonAsync(
                $"/friend/{searchLoveTryId}/compare?sessionId={sessionId}",
                PrepareContendersRequest(firstContender, secondContender))
            .Result;

        if (!webResponse.IsSuccessStatusCode)
        {
            throw new SecretaryProblemException();
        }
        
        var responseContext = webResponse.Content.ReadAsStringAsync().Result;
        var compareContendersResponse = JsonSerializer.Deserialize<CompareContendersResponse>(responseContext);
        var nameSplit = ContenderNamesConverter
            .parseFullNameFirstSecond(compareContendersResponse?.ContenderName);
            
        if(nameSplit.first == null || nameSplit.second == null)
            throw new SecretaryProblemException(ErrorType.InvalidContender());
            
        return new RatedContender(nameSplit.first, nameSplit.second, 0);
    }

    public int GetFinalResult(RatedContender? ratedContender)
    {
        throw new NotImplementedException();
    }

    private CompareContendersRequest PrepareContendersRequest(RatedContender? firstContender,
        RatedContender? secondContender)
    {
        return new CompareContendersRequest(firstContender?.GetDetailsBasic(),
            secondContender?.GetDetailsBasic());
    }
}