using YamlDotNet.Serialization;

namespace ClashGui.Cli.ClashConfigs;

public class Tun
{
    [YamlMember(Alias="enable")]
    public bool Enable { get; set; }
    [YamlMember(Alias="stack")]
    public string Stack { get; set; }
    [YamlMember(Alias="dns-hijack")]
    public string[] DnsHijack { get; set; }
    [YamlMember(Alias="auto-route")]
    public bool AutoRoute { get; set; }
    [YamlMember(Alias="auto-detect-interface")]
    public bool AutoDetectInterface { get; set; }
}