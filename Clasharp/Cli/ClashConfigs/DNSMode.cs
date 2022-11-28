using YamlDotNet.Serialization;

namespace Clasharp.Cli.ClashConfigs;

public enum DNSMode
{
    DNSNormal,
    DNSFakeIP,
    DNSMapping,
}