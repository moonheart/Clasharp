using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashGui.Models;

public class ProxyList
{
    [JsonPropertyName("proxies")]
    public Dictionary<string, ProxyGroup> Proxies { get; set; } = new();
}