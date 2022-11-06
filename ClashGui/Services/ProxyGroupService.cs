using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ProxyGroupService : BaseListService<ProxyGroup>, IProxyGroupService
{
    private static readonly string[] NotShownProxyGroups = {"DIRECT", "GLOBAL", "REJECT", "COMPATIBLE", "PASS"};
    private static ProxyGroupComparer _proxyGroupComparer = new();

    public ProxyGroupService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<List<ProxyGroup>> GetObj()
    {
        var proxyData = await _clashApiFactory.Get().GetProxyGroups();
        return proxyData?.Proxies?.Values.Where(d => !NotShownProxyGroups.Contains(d.Name)).ToList() ??
               new List<ProxyGroup>();
    }

    protected override bool ObjEquals(List<ProxyGroup> oldObj, List<ProxyGroup> newObj)
    {
        return oldObj.SequenceEqual(newObj, _proxyGroupComparer);
    }


    private class ProxyGroupComparer : IEqualityComparer<ProxyGroup>
    {
        public bool Equals(ProxyGroup? x, ProxyGroup? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.All.SequenceEqual(y.All)
                   && x.Name == y.Name
                   && x.Now == y.Now
                   && x.Type == y.Type
                   && x.Udp == y.Udp;
        }

        public int GetHashCode(ProxyGroup obj)
        {
            return HashCode.Combine(obj.All, obj.Name, obj.Now, obj.Type, obj.Udp);
        }
    }

    public Task SelectProxy(string group, string name)
    {
        return _clashApiFactory.Get().SelectProxy(group, new UpdateProxyRequest {Name = name});
    }
}