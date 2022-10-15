using System.Collections.Generic;
using System.Linq;

namespace ClashGui.Clash.Models.Proxies;

public class ProxyGroup
{
    public List<string> All { get; set; } = new();

    public List<ProxyHistory> History { get; set; } = new();

    public ProxyHistory? LatestHistory => History.LastOrDefault();

    public string Name { get; set; } = null!;

    public string? Now { get; set; }

    public string Type { get; set; } = null!;

    public bool Udp { get; set; }
}