using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Controls;
using ClashGui.Models;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyListViewModel : ViewModelBase
{
    public ProxyListViewModel()
    {
        this.WhenAnyValue(d => d.ProxyProviders)
            .Subscribe(d => this.RaisePropertyChanged(nameof(Count)));
        ProxyProviders = new ObservableCollection<ProxyProvider>();
        ProxyGropups = new ObservableCollection<ProxyGroup>();
    }

    public ObservableCollection<ProxyGroup> ProxyGropups { get; }

    public ObservableCollection<ProxyProvider> ProxyProviders { get; }

    public int Count => ProxyProviders.Count;
}