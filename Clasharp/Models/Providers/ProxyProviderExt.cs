using System.Collections.Generic;
using System.Linq;
using Clasharp.Clash.Models.Providers;
using Clasharp.Models.Proxies;

namespace Clasharp.Models.Providers;

public sealed class ProxyProviderExt
{
    public ProxyProviderExt(ProxyProvider proxyProvider)
    {
        ProxyProvider = proxyProvider;
        Proxies = proxyProvider.Proxies.Select(pg => new ProxyGroupExt(pg)).ToList();
    }

    public ProxyProvider ProxyProvider { get; }

    public List<ProxyGroupExt> Proxies { get; }

    protected bool Equals(ProxyProviderExt other)
    {
        return ProxyProvider.Equals(other.ProxyProvider);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProxyProviderExt) obj);
    }

    public override int GetHashCode()
    {
        return ProxyProvider.GetHashCode();
    }
}