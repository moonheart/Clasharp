using System.Text.Json.Serialization;

namespace ClashGui.Clash.Models;

public class Tun
{
    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    [JsonPropertyName("device")]
    public string Device { get; set; }

    [JsonPropertyName("stack")]
    public string Stack { get; set; }

    [JsonPropertyName("dns-hijack")]
    public string[] DnsHijack { get; set; }

    [JsonPropertyName("auto-route")]
    public bool AutoRoute { get; set; }

    [JsonPropertyName("auto-detect-interface")]
    public bool AutoDetectInterface { get; set; }

    [JsonPropertyName(("inet4_address"))]
    public string[] Inet4Address { get; set; }

    [JsonPropertyName(("inet6_address"))]
    public string[] Inet6Address { get; set; }
}