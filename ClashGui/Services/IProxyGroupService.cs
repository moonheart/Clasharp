using System.Collections.Generic;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyGroupService : IObservalbeObjService<List<ProxyGroup>>, IAutoFreshable
{
}