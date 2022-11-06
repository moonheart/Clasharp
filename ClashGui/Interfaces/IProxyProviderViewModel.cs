using System.Collections.Generic;
using System.Reactive;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Models.Proxies;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProxyProviderViewModel:IViewModelBase
{
    bool IsLoading { get; }
    
    ReactiveCommand<string, Unit> CheckCommand { get; }
    
    ReactiveCommand<string, Unit> UpdateCommand { get; }

    ProxyProvider ProxyProvider { get; }
    
    List<ProxyGroupExt> Proxies { get; }
    
}