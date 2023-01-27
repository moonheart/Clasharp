using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Cli;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public class ProxyGroupService : BaseListService<ProxyGroup, string>, IProxyGroupService
{
    private static readonly string[] NotShownProxyGroups = {"DIRECT", "GLOBAL", "REJECT", "COMPATIBLE", "PASS"};

    public ProxyGroupService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<List<ProxyGroup>> GetObj()
    {
        var proxyData = await _clashApiFactory.Get().GetProxyGroups();
        return proxyData?.Proxies?.Values.Where(d => !NotShownProxyGroups.Contains(d.Name)).ToList() ??
               new List<ProxyGroup>();
    }

    public Task SelectProxy(string group, string name)
    {
        return _clashApiFactory.Get().SelectProxy(group, new UpdateProxyRequest {Name = name});
    }

    protected override string GetUniqueKey(ProxyGroup obj)
    {
        return obj.Name;
    }
}