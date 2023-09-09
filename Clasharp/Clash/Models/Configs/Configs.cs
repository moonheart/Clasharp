using System.Collections.Generic;

namespace Clasharp.Clash.Models;

public class Configs
{
    public int Port { get; set; }
    public int SocksPort { get; set; }
    public int RedirPort { get; set; }
    public int TproxyPort { get; set; }
    public int MixedPort { get; set; }
    public List<object> Authentication { get; set; } = new();
    public bool AllowLan { get; set; }
    public string BindAddress { get; set; } = "";
    public bool InboundTfo { get; set; }
    public string Mode { get; set; } = "";
    public bool UnifiedDelay { get; set; }
    public string LogLevel { get; set; } = "";
    public bool Ipv6 { get; set; }
    public string InterfaceName { get; set; } = "";
    public string GeodataMode { get; set; } = "";
    public string GeodataLoader { get; set; } = "";
    public bool TCPConcurrent { get; set; } = false;
    public bool EnableProcess { get; set; }
    public Tun Tun { get; set; } = new();
    public bool Sniffing { get; set; }
}