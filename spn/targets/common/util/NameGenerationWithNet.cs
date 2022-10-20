using System.Net;
using System.Text.Json;
using task_1.targets.common.exceptions;

namespace task_1.targets;

public class NameGenerationWithNet
{
    private const string ApiKeyFileName = "../../../../spn/targets/resources/x-api-key.txt";
    private const string FullNameDelimiter = " ";
    private readonly HttpClient _httpClient = new HttpClient();

    public List<(string First, string Second)> GenerateFullNames(int quantity)
    {
        var fullNames = new List<(string First, string Second)>();
        var fullNamesResponse = GetNFullNamesResponse(quantity);

        if (fullNamesResponse.IsSuccessStatusCode)
        {
            var names = GetNFullNamesFromResponse(fullNamesResponse, quantity);
            fullNames.AddRange(names.Select(name => name.Split(FullNameDelimiter))
                .Select(splittedName => (splittedName[0], splittedName[1])));
        }
        else
        {
            Console.WriteLine($"There is problem while getting fullNames from Net: {fullNamesResponse.StatusCode}");
            throw new SecretaryProblemException(ErrorType.RandomFullNameNetError());
        }

        return fullNames;
    }

    private List<string> GetNFullNamesFromResponse(HttpResponseMessage fullNamesResponse, int quantity)
    {
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | 
                                                SecurityProtocolType.Tls11 | 
                                                SecurityProtocolType.Tls;
        var responseContent = fullNamesResponse.Content.ReadAsStringAsync().Result;
        var names = JsonSerializer.Deserialize<List<string>>(responseContent);

        if (names == null || names.Count != quantity)
        {
            throw new SecretaryProblemException(ErrorType.RandomFullNameNetError());
        }

        return names;
    }

    private HttpResponseMessage GetNFullNamesResponse(int quantity)
    {
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", File.ReadLines(ApiKeyFileName).First());
        return _httpClient.GetAsync(GenerateRequestForGettingFullNames(quantity)).Result;
    }

    private string GenerateRequestForGettingFullNames(int quantity)
    {
        return $"https://randommer.io/api/Name?quantity={quantity}&nameType=fullname";
    }
}