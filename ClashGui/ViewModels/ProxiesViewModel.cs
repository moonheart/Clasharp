using System;
using System.Collections.ObjectModel;
using ClashGui.Interfaces;
using ReactiveUI;

namespace ClashGui.ViewModels;

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