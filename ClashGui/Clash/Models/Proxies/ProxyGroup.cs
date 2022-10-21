using System.Collections.Generic;
using System.Linq;
using ClashGui.Models.Proxies;

namespace ClashGui.Clash.Models.Proxies;

public class ProxyGroup
{
    public List<string> All { get; set; } = new();
    public List<ProxyHistory> History { get; set; } = new();

    public string Name { get; set; } = null!;

    public string? Now { get; set; }

    public ProxyGroupType Type { get; set; }

    public bool Udp { get; set; }
}