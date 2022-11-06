using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
        
        CheckCommand = ReactiveCommand.CreateFromTask<string>(async name =>
            await proxyProviderService.HealthCheckProxyProvider(name));
        UpdateCommand = ReactiveCommand.CreateFromTask<string>(async name =>
            await proxyProviderService.UpdateProxyProvider(name));
    }

    [ObservableAsProperty]
    public List<IProxyProviderViewModel>? ProxyProviders { get; }
    
    
    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }

}