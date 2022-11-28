using System.Collections.Generic;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Providers;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public interface IProxyProviderService : IObservableListService<ProxyProvider, string>, IAutoFreshable
{
    Task HealthCheckProxyProvider(string name);
    Task UpdateProxyProvider(string name);
}