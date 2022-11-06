using System.Collections.Generic;
using System.Linq;
using ClashGui.Common.ApiModels.Proxies;

namespace ClashGui.Models.Proxies;

public class ProxyGroupExt
{
    public ProxyGroupExt(ProxyGroup proxyGroup)
    {
        ProxyGroup = proxyGroup;
        SelectProxies = proxyGroup.All.Select(p => new SelectProxy {Group = proxyGroup.Name, Proxy = p}).ToList();
        LatestHistory = GetLatestHistory();
        SelectedProxy = ProxyGroup.Now == null
            ? null
            : new SelectProxy() {Group = ProxyGroup.Name, Proxy = ProxyGroup.Now};
    }

    public ProxyGroup ProxyGroup { get; }

    public List<SelectProxy> SelectProxies { get; }
    
    public SelectProxy? SelectedProxy { get; set; }

    private string GetLatestHistory()
    {
        var h = ProxyGroup.History.LastOrDefault();
        if (h == null) return "";
        if (h.Delay == 0) return "timeout";
        return $"{h.Delay}ms";
    }

    public string? LatestHistory { get; }
}