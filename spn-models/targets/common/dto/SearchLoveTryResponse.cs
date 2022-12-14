using System.Text.Json.Serialization;

namespace spn_http_princess.targets.dto;

public class SearchLoveTryResponse
{
    [JsonPropertyName("expected_rank")] public int ExpectedRank { get; set; }
    [JsonPropertyName("actual_rank")] public int ActualRank { get; set; }

    [JsonPropertyName("selected_contender_in_rerun")]
    public string? SelectedContenderInRerun { get; set; }

    public SearchLoveTryResponse(int expectedRank, int actualRank, string? selectedContenderInRerun)
    {
        ExpectedRank = expectedRank;
        ActualRank = actualRank;
        SelectedContenderInRerun = selectedContenderInRerun;
    }
}