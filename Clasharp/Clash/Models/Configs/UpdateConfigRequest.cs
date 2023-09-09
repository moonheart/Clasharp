using System.Text.Json.Serialization;

namespace Clasharp.Clash.Models;

public class UpdateConfigRequest
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("payload")]
    public string Payload { get; set; } = string.Empty;
}