using System;
using System.Text.Json.Serialization;

namespace ClashGui.Models;

public class ProxyHistory
{
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }
    
    [JsonPropertyName("delay")]
    public int Delay { get; set; }
}