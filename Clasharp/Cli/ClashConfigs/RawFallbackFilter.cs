using System;
using YamlDotNet.Serialization;

namespace Clasharp.Cli.ClashConfigs;

public class RawFallbackFilter
{
    [YamlMember(Alias = "geoip")]
    public bool GeoIP { get; set; }

    [YamlMember(Alias = "geoip-code")]
    public string GeoIPCode { get; set; } = string.Empty;

    [YamlMember(Alias = "ipcidr")]
    public string[] IPCIDR { get; set; } = Array.Empty<string>();

    [YamlMember(Alias = "domain")]
    public string[] Domain { get; set; } = Array.Empty<string>();
}