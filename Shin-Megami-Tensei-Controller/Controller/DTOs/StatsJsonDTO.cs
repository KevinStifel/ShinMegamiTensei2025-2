using System.Text.Json.Serialization;

namespace Shin_Megami_Tensei;

public class StatsJsonDto
{
    [JsonPropertyName("HP")]
    public int HP { get; set; }

    [JsonPropertyName("MP")]
    public int MP { get; set; }

    [JsonPropertyName("Str")]
    public int Str { get; set; }

    [JsonPropertyName("Skl")]
    public int Skl { get; set; }

    [JsonPropertyName("Mag")]
    public int Mag { get; set; }

    [JsonPropertyName("Spd")]
    public int Spd { get; set; }

    [JsonPropertyName("Lck")]
    public int Lck { get; set; }
}