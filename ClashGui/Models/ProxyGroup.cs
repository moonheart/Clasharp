using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashGui.Models;

public class ProxyGroup
{
    [JsonPropertyName("all")]
    public List<string> All { get; set; } = new();
    
    
    [JsonPropertyName("history")]
    public List<ProxyHistory> History { get; set; } = new();

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    
    [JsonPropertyName("now")]
    public string? Now { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;
    
    [JsonPropertyName("udp")]
    public bool Udp { get; set; }
}