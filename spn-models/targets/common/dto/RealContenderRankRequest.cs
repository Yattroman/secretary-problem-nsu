using System.Text.Json.Serialization;

namespace spn_models.targets.common.dto;

public class RealContenderRankRequest
{
    [JsonPropertyName("name")]
    public string? ContenderName { get; set; }

    public RealContenderRankRequest(string? contenderName)
    {
        ContenderName = contenderName;
    }
}