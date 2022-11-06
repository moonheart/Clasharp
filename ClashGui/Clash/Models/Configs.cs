using System.Collections.Generic;

namespace ClashGui.Clash.Models;

public class Configs
{
    public int Port { get; set; }


    public int SocksPort { get; set; }


    public int RedirPort { get; set; }


    public int TproxyPort { get; set; }


    public int MixedPort { get; set; }


    public List<object> Authentication { get; set; }


    public bool AllowLan { get; set; }


    public string BindAddress { get; set; }


    public string Mode { get; set; }


    public string LogLevel { get; set; }


    public bool Ipv6 { get; set; }
}