using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ClashGui.Cli.ClashConfigs;

public class RawDNS
{
    [YamlMember(Alias = "enable")]
    public bool Enable { get; set; }

    [YamlMember(Alias = "ipv6")]
    public bool IPv6 { get; set; }

    [YamlMember(Alias = "use-hosts")]
    public bool UseHosts { get; set; }

    [YamlMember(Alias = "nameserver")]
    public string[] NameServer { get; set; }

    [YamlMember(Alias = "fallback")]
    public string[] Fallback { get; set; }

    [YamlMember(Alias = "fallback-filter")]
    public RawFallbackFilter FallbackFilter { get; set; }

    [YamlMember(Alias = "listen")]
    public string Listen { get; set; }

    [YamlMember(Alias = "enhanced-mode")]
    public string EnhancedMode { get; set; }

    [YamlMember(Alias = "fake-ip-range")]
    public bool FakeIPRange { get; set; }

    [YamlMember(Alias = "fake-ip-filter")]
    public string[] FakeIPFilter { get; set; }

    [YamlMember(Alias = "default-nameserver")]
    public string[] DefaultNameserver { get; set; }

    [YamlMember(Alias = "nameserver-policy")]
    public Dictionary<string, string> NameServerPolicy { get; set; }
}