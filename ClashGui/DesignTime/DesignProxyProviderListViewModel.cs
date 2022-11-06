using System.Collections.Generic;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public List<IProxyProviderViewModel> ProxyProviders => new List<IProxyProviderViewModel>()
    {
        new DesignProxyProviderViewModel()
    };

    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}