using System.Collections.Generic;
using ClashGui.Common.ApiModels.Proxies;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyGroupService : IObservalbeObjService<List<ProxyGroup>>, IAutoFreshable
{
}