using System.Text.Json.Serialization;

namespace spn_models.targets.common.dto;

public class CompareContendersRequest
{
    [JsonPropertyName("name1")]
    public string? ContenderAName { get; set; }

    [JsonPropertyName("name2")]
    public string? ContenderBName { get; set; }

    public CompareContendersRequest(string? contenderAName, string? contenderBName)
    {
        ContenderAName = contenderAName;
        ContenderBName = contenderBName;
    }
}