using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClashGui.Controls;
using ClashGui.Models;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyListViewModel : ViewModelBase
{
    public ProxyListViewModel()
    {
        this.WhenAnyValue(d => d.ProxyGroups)
            .Subscribe(d => this.RaisePropertyChanged(nameof(Count)));
        ProxyGroups = new ObservableCollection<ProxyGroup>();
    }

    public ObservableCollection<ProxyGroup> ProxyGroups { get; }

    public int Count => ProxyGroups.Count;
}