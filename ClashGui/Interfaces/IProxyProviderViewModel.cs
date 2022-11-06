using System.Collections.Generic;
using System.Reactive;
using ClashGui.Clash.Models.Providers;
using ClashGui.Models.Proxies;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProxyProviderViewModel:IViewModelBase
{
    bool IsLoading { get; }

    ProxyProvider ProxyProvider { get; }
    
    List<ProxyGroupExt> Proxies { get; }
    
}