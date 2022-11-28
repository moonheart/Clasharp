using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Clasharp.Interfaces;
using Clasharp.Services;
using DynamicData;
using ReactiveUI;

namespace Clasharp.ViewModels;

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