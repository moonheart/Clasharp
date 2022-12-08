using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Clasharp.Cli.ClashConfigs;

public class RawConfig
{
    [YamlMember(Alias = "port")]
    public int? Port { get; set; }

    [YamlMember(Alias = "socks-port")]
    public int? SocksPort { get; set; }

    [YamlMember(Alias = "redir-port")]
    public int? RedirPort { get; set; }

    [YamlMember(Alias = "tproxy-port")]
    public int? TProxyPort { get; set; }

    [YamlMember(Alias = "mixed-port")]
    public int? MixedPort { get; set; }

    [YamlMember(Alias = "authentication")]
    public string? Authentication { get; set; }

    [YamlMember(Alias = "allow-lan")]
    public bool? AllowLan { get; set; }

    [YamlMember(Alias = "bind-address")]
    public string? BindAddress { get; set; }

    [YamlMember(Alias = "mode")]
    public TunnelMode? Mode { get; set; }

    [YamlMember(Alias = "log-level")]
    public LogLevel? LogLevel { get; set; }

    [YamlMember(Alias = "ipv6")]
    public bool? IPv6 { get; set; }
    
    [YamlMember(Alias = "sniffer")]
    public RawSniffer? Sniffer { get; set; }

    [YamlMember(Alias = "external-controller")]
    public string? ExternalController { get; set; }

    [YamlMember(Alias = "external-ui")]
    public string? ExternalUI { get; set; }

    [YamlMember(Alias = "secret")]
    public string? Secret { get; set; }

    [YamlMember(Alias = "interface-name")]
    public string? Interface { get; set; }

    [YamlMember(Alias = "routing-mark")]
    public int? RoutingMark { get; set; }

    [YamlMember(Alias = "proxy-providers")]
    public Dictionary<string, Dictionary<string, object>>? ProxyProvider { get; set; }

    [YamlMember(Alias = "rule-providers")]
    public Dictionary<string, Dictionary<string, object>>? RuleProvider { get; set; }

    [YamlMember(Alias = "tun")]
    public Tun? Tun { get; set; }

    [YamlMember(Alias = "hosts")]
    public Dictionary<string, string>? Hosts { get; set; }

    [YamlMember(Alias = "dns")]
    public RawDNS? DNS { get; set; }

    [YamlMember(Alias = "experimental")]
    public Experimental? Experimental { get; set; }

    [YamlMember(Alias = "profile")]
    public Profile? Profile { get; set; }

    [YamlMember(Alias = "proxies")]
    public Dictionary<string, object>[]? Proxy { get; set; }

    [YamlMember(Alias = "proxy-groups")]
    public Dictionary<string, object>[]? ProxyGroup { get; set; }

    [YamlMember(Alias = "rules")]
    public string[]? Rule { get; set; }
}