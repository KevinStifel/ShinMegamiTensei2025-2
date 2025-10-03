using System.Text.Json.Serialization;

namespace Shin_Megami_Tensei;

public class SkillJsonDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("cost")]
    public int Cost { get; set; }

    [JsonPropertyName("power")]
    public int Power { get; set; }

    [JsonPropertyName("target")]
    public string Target { get; set; } = "";

    [JsonPropertyName("hits")]
    public string Hits { get; set; } = "";

    [JsonPropertyName("effect")]
    public string Effect { get; set; } = "";
}