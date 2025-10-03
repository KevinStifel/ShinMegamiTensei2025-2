using System.Text.Json.Serialization;

namespace Shin_Megami_Tensei;

public class SamuraiJsonDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("stats")]
    public StatsJsonDto Stats { get; set; } = new();

    [JsonPropertyName("affinity")]
    public Dictionary<string, string> Affinity { get; set; } = new();
}