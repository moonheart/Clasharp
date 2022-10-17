using System.Collections.Generic;
using System.Linq;
using ClashGui.Clash.Models.Providers;
using ClashGui.Models.Proxies;

namespace ClashGui.Models.Providers;

public class ProxyProviderExt
{
    public ProxyProviderExt(ProxyProvider proxyProvider)
    {
        ProxyProvider = proxyProvider;
        Proxies = proxyProvider.Proxies.Select(pg => new ProxyGroupExt(pg)).ToList();
    }

    public ProxyProvider ProxyProvider { get; }

    public List<ProxyGroupExt> Proxies { get; }
}