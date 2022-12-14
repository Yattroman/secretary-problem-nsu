using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using spn_models.targets.common.dto;
using spn_models.targets.common.exceptions;
using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;
using spn_models.targets.common.util;

namespace spn_http_princess.targets.clients;

public class SimulatedHall : ISimulatedHall
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SimulatedHall> _logger;

    public SimulatedHall(HttpClient httpClient, ILogger<SimulatedHall> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpClient.BaseAddress = new Uri("http://localhost:5034");
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
    }

    public RatedContender? ReturnNextContender([Optional] int searchLoveTryId, [Optional] string sessionId)
    {
        var webResponse = _httpClient.PostAsJsonAsync(
                $"/hall/{searchLoveTryId}/next?sessionId={sessionId}",
                new {})
            .Result;

        if (!webResponse.IsSuccessStatusCode)
        {
            throw new SecretaryProblemException();
        }

        var responseContext = webResponse.Content.ReadAsStringAsync().Result;
        var nextContendersResponse = JsonSerializer.Deserialize<NextContenderResponse>(responseContext);

        if (nextContendersResponse?.ContenderName == null)
        {
            return null;
        }
        
        var nameSplit = ContenderNamesConverter
            .parseFullNameFirstSecond(nextContendersResponse?.ContenderName);

        if (nameSplit.first == null || nameSplit.second == null)
            throw new SecretaryProblemException(ErrorType.InvalidContender());

        return new RatedContender(nameSplit.first, nameSplit.second, 0);
    }

    public void AddNewContender(RatedContender contender)
    {
        _logger.LogInformation("Method is not supported for SimulatedHallClient");
        throw new NotSupportedException();
    }

    public bool IsNoContendersInHall()
    {
        _logger.LogInformation("Method is not supported for SimulatedHallClient");
        throw new NotSupportedException();
    }

    public void ClearHall([Optional] string sessionId)
    {
        var webResponse = _httpClient.PostAsJsonAsync(
                $"/hall/reset?sessionId={sessionId}",
                new {})
            .Result;

        if (!webResponse.IsSuccessStatusCode)
        {
            throw new SecretaryProblemException();
        }
    }

    public int GetSelectedContenderRank([Optional] int searchLoveTryId, [Optional] string sessionId)
    {
        var webResponse = _httpClient.PostAsJsonAsync(
                $"/hall/{searchLoveTryId}/select?sessionId={sessionId}",
                new {})
            .Result;

        if (!webResponse.IsSuccessStatusCode)
        {
            throw new SecretaryProblemException();
        }

        var responseContext = webResponse.Content.ReadAsStringAsync().Result;
        var selectedContendersResponse = JsonSerializer.Deserialize<ContenderRankResponse>(responseContext);
        var rank = selectedContendersResponse?.ContenderRank;

        return rank?? -99;
    }
    
    public int ShowRealContenderRank([Optional] int searchLoveTryId, [Optional] string sessionId, string? contenderName)
    {
        var webResponse = _httpClient.PostAsJsonAsync(
                $"/hall/{searchLoveTryId}/showRealContenderRank?sessionId={sessionId}",
                new RealContenderRankRequest(contenderName))
            .Result;

        if (!webResponse.IsSuccessStatusCode)
        {
            throw new SecretaryProblemException();
        }

        var responseContext = webResponse.Content.ReadAsStringAsync().Result;
        var selectedContendersResponse = JsonSerializer.Deserialize<ContenderRankResponse>(responseContext);
        var rank = selectedContendersResponse?.ContenderRank;
        
        return rank is null or -99 ? IChoiceStrategy.ParticularFailure :
            rank.Value < IChoiceStrategy.SuccessBorder ? IChoiceStrategy.FullFailure : rank.Value;
    }
}