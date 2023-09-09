namespace Clasharp.Clash.Models.Connections;

public class Metadata
{
    public string Network { get; set; } = string.Empty;


    public string Type { get; set; } = string.Empty;


    public string SourceIP { get; set; } = string.Empty;


    public string DestinationIP { get; set; } = string.Empty;


    public string SourcePort { get; set; } = string.Empty;


    public string DestinationPort { get; set; } = string.Empty;


    public string? Host { get; set; }


    public string DnsMode { get; set; } = string.Empty;


    public string ProcessPath { get; set; } = string.Empty;
}