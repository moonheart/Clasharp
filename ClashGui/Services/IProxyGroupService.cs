using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyGroupService : IObservableListService<ProxyGroup, string>, IAutoFreshable
{
    Task SelectProxy(string group, string name);
}