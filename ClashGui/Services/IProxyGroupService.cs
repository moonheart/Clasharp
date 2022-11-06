using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyGroupService : IObservalbeObjService<List<ProxyGroup>>, IAutoFreshable
{
    Task SelectProxy(string group, string name);
}