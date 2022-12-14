using System.Text.Json.Serialization;

namespace spn_models.targets.common.dto;

public class NextContenderResponse
{
    [JsonPropertyName("name")]
    public string? ContenderName { get; set; }

    public NextContenderResponse(string? contenderName)
    {
        ContenderName = contenderName;
    }
}