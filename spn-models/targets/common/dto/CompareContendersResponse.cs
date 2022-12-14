using System.Text.Json.Serialization;

namespace spn_models.targets.common.dto;

public class CompareContendersResponse
{
    [JsonPropertyName("name")]
    public string? ContenderName { get; set; }

    public CompareContendersResponse(string? contenderName)
    {
        ContenderName = contenderName;
    }
}