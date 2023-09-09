using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Clasharp.Cli.ClashConfigs;

public class RawDNS
{
    [YamlMember(Alias = "enable")]
    public bool Enable { get; set; }

    [YamlMember(Alias = "ipv6")]
    public bool IPv6 { get; set; }

    [YamlMember(Alias = "use-hosts")]
    public bool UseHosts { get; set; }

    [YamlMember(Alias = "nameserver")]
    public string[] NameServer { get; set; } = Array.Empty<string>();

    [YamlMember(Alias = "fallback")]
    public string[] Fallback { get; set; } = Array.Empty<string>();

    [YamlMember(Alias = "fallback-filter")]
    public RawFallbackFilter FallbackFilter { get; set; } = new();

    [YamlMember(Alias = "listen")]
    public string Listen { get; set; } = string.Empty;

    [YamlMember(Alias = "enhanced-mode")]
    public string EnhancedMode { get; set; } = string.Empty;

    [YamlMember(Alias = "fake-ip-range")]
    public string FakeIPRange { get; set; } = string.Empty;

    [YamlMember(Alias = "fake-ip-filter")]
    public string[] FakeIPFilter { get; set; } = Array.Empty<string>();

    [YamlMember(Alias = "default-nameserver")]
    public string[] DefaultNameserver { get; set; } = Array.Empty<string>();

    [YamlMember(Alias = "nameserver-policy")]
    public Dictionary<string, string> NameServerPolicy { get; set; } = new();
}