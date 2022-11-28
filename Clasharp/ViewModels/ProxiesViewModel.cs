using System;
using System.Collections.ObjectModel;
using Clasharp.Interfaces;
using ReactiveUI;

namespace Clasharp.ViewModels;

public class ProxiesViewModel : ViewModelBase, IProxiesViewModel
{
    public override string Name => "Proxies";
    public ProxiesViewModel(IProxyGroupListViewModel proxyGroupListViewModel, IProxyProviderListViewModel proxyProviderListViewModel)
    {
        ProxyGroupListViewModel = proxyGroupListViewModel;
        ProxyProviderListViewModel = proxyProviderListViewModel;
    }

    public IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    public IProxyProviderListViewModel ProxyProviderListViewModel { get; }


}