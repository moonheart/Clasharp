using YamlDotNet.Serialization;

namespace Clasharp.Cli.ClashConfigs;

public class RawSniffer
{
    [YamlMember(Alias = "enable")]
    public bool Enable{get;set;}              
    [YamlMember(Alias = "sniffing")]
    public string[] Sniffing{get;set;}        
    [YamlMember(Alias = "force-domain")]
    public string[] ForceDomain{get;set;}     
    [YamlMember(Alias = "skip-domain")]
    public string[] SkipDomain{get;set;}      
    [YamlMember(Alias = "port-whitelist")]
    public string[] Ports{get;set;}           
    [YamlMember(Alias = "force-dns-mapping")]
    public bool ForceDnsMapping{get;set;}     
    [YamlMember(Alias = "parse-pure-ip")]
    public bool ParsePureIp{get;set;}         

}