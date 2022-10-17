using System;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyListViewModel : ViewModelBase
{
    public ProxyListViewModel()
    {
        ProxyGroupListViewModel = new ();
        ProxyProviderListViewModel = new ();
    }

    public ProxyGroupListViewModel ProxyGroupListViewModel { get; }
    public ProxyProviderListViewModel ProxyProviderListViewModel { get; }


}