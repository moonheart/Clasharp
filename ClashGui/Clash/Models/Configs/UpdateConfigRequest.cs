using System.Text.Json.Serialization;

namespace ClashGui.Clash.Models;

public class UpdateConfigRequest
{
    [JsonPropertyName("path")]
    public string Path { get; set; }
    [JsonPropertyName("payload")]
    public string Payload { get; set; }
}