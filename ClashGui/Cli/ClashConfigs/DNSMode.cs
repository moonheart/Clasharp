using YamlDotNet.Serialization;

namespace ClashGui.Cli.ClashConfigs;

public enum DNSMode
{
    DNSNormal,
    DNSFakeIP,
    DNSMapping,
}