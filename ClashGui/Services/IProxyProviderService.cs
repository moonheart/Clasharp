using System.Collections.Generic;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyProviderService : IObservalbeObjService<List<ProxyProvider>>, IAutoFreshable
{
}