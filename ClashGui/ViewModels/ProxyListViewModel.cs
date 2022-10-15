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
        ProxyProviders = new ObservableCollection<ProxyProvider>();
        ProxyGropups = new ObservableCollection<ProxyGroup>();
    }

    public ObservableCollection<ProxyGroup> ProxyGropups { get; }

    public ObservableCollection<ProxyProvider> ProxyProviders { get; }

}