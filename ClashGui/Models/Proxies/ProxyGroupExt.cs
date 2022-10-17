using System.Collections.Generic;
using System.Linq;
using ClashGui.Clash.Models.Proxies;

namespace ClashGui.Models.Proxies;

public class ProxyGroupExt
{
    public ProxyGroupExt(ProxyGroup proxyGroup)
    {
        ProxyGroup = proxyGroup;
        SelectProxies = proxyGroup.All.Select(p => new SelectProxy {Group = proxyGroup.Name, Proxy = p}).ToList();
        SelectedIndex = proxyGroup.Now == null ? null : proxyGroup.All.IndexOf(proxyGroup.Now);
        LatestHistory = GetLatestHistory();
    }

    public ProxyGroup ProxyGroup { get; }

    public List<SelectProxy> SelectProxies { get; }

    public int? SelectedIndex { get; }

    private string GetLatestHistory()
    {
        var h = ProxyGroup.History.LastOrDefault();
        if (h == null) return "";
        if (h.Delay == 0) return "timeout";
        return $"{h.Delay}ms";
    }

    public string? LatestHistory { get; }
}