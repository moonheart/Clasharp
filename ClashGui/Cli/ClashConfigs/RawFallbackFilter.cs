using YamlDotNet.Serialization;

namespace ClashGui.Cli.ClashConfigs;

public class RawFallbackFilter
{
    [YamlMember(Alias = "geoip")]
    public bool GeoIP { get; set; }

    [YamlMember(Alias = "geoip-code")]
    public string GeoIPCode { get; set; }

    [YamlMember(Alias = "ipcidr")]
    public string[] IPCIDR { get; set; }

    [YamlMember(Alias = "domain")]
    public string[] Domain { get; set; }
}