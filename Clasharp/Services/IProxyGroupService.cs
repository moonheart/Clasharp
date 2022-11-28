using System.Collections.Generic;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public interface IProxyGroupService : IObservableListService<ProxyGroup, string>, IAutoFreshable
{
    Task SelectProxy(string group, string name);
}