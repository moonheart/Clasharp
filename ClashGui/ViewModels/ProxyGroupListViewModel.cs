using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ClashGui.Interfaces;
using ClashGui.Services;
using DynamicData;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public ProxyGroupListViewModel(IProxyGroupService proxyGroupService)
    {
        proxyGroupService.List
            .Transform(d => new ProxyGroupViewModel(d, proxyGroupService.SelectProxy) as IProxyGroupViewModel)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
    }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<IProxyGroupViewModel> ProxyGroupViewModels => _items;

    private ReadOnlyObservableCollection<IProxyGroupViewModel> _items;
}