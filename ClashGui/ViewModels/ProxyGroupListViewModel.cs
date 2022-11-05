using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ClashGui.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace ClashGui.ViewModels;

public class ProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public ProxyGroupListViewModel(IProxyGroupService proxyGroupService)
    {
        proxyGroupService.Obj
            .Select(d => d.Select(x => new ProxyGroupViewModel(x) as IProxyGroupViewModel).ToList())
            .ToPropertyEx(this, d => d.ProxyGroupViewModels);
    }

    [ObservableAsProperty]
    public List<IProxyGroupViewModel> ProxyGroupViewModels { get; }
}