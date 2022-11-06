using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Interfaces;
using ClashGui.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public ProxyProviderListViewModel(IProxyProviderService proxyProviderService)
    {
        proxyProviderService.Obj
            .Select(d => d.Select(x => new ProxyProviderViewModel(x) as IProxyProviderViewModel).ToList())
            .ToPropertyEx(this, d => d.ProxyProviders);
    }

    [ObservableAsProperty]
    public List<IProxyProviderViewModel>? ProxyProviders { get; }

}