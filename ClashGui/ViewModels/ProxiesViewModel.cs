using System;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxiesViewModel : ViewModelBase, IProxiesViewModel
{
    public override string Name => "Proxies";
    public ProxiesViewModel()
    {
        ProxyGroupListViewModel = new ProxyGroupListViewModel();
        ProxyProviderListViewModel = new ProxyProviderListViewModel();
    }

    public IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    public IProxyProviderListViewModel ProxyProviderListViewModel { get; }


}