using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Clasharp.Clash.Models.Proxies;
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
            .Filter(d => d.Type != ProxyGroupType.Vmess &&
                         d.Type != ProxyGroupType.Trojan &&
                         d.Type != ProxyGroupType.Shadowsocks &&
                         d.Type != ProxyGroupType.Socks5 &&
                         d.Type != ProxyGroupType.ShadowsocksR
            )
            .SortBy(d => d.Name)
            .ForEachChange(change =>
            {
                if (change.Previous.HasValue)
                {
                    change.Current.Expanded = change.Previous.Value.Expanded;
                }
            })
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
    }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<ProxyGroupModel> ProxyGroupViewModels => _items;

    private readonly ReadOnlyObservableCollection<ProxyGroupModel> _items;
}