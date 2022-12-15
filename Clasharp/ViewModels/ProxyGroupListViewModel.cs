using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            .Transform(d => new ProxyGroupModel(d, proxyGroupService.SelectProxy))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
    }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<ProxyGroupModel> ProxyGroupViewModels => _items;

    private ReadOnlyObservableCollection<ProxyGroupModel> _items;
}