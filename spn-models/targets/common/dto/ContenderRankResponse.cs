using System.Text.Json.Serialization;

namespace spn_models.targets.common.dto;

public class ContenderRankResponse
{
    [JsonPropertyName("rank")]
    public int? ContenderRank { get; set; }

    public ContenderRankResponse(int? contenderRank)
    {
        ContenderRank = contenderRank;
    }
}