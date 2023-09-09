using System;
using System.Text.Json.Serialization;

namespace Clasharp.Clash.Models;

public class Tun
{
    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    [JsonPropertyName("device")]
    public string Device { get; set; } = string.Empty;

    [JsonPropertyName("stack")]
    public string Stack { get; set; } = string.Empty;

    [JsonPropertyName("dns-hijack")]
    public string[] DnsHijack { get; set; } = Array.Empty<string>();

    [JsonPropertyName("auto-route")]
    public bool AutoRoute { get; set; }

    [JsonPropertyName("auto-detect-interface")]
    public bool AutoDetectInterface { get; set; }

    [JsonPropertyName(("inet4_address"))]
    public string[] Inet4Address { get; set; } = Array.Empty<string>();

    [JsonPropertyName(("inet6_address"))]
    public string[] Inet6Address { get; set; } = Array.Empty<string>();
}