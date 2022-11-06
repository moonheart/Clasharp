using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Providers;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyProviderService : IObservalbeObjService<List<ProxyProvider>>, IAutoFreshable
{
    Task HealthCheckProxyProvider(string name);
    Task UpdateProxyProvider(string name);
}