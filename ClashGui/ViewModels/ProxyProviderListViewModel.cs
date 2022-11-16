using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Interfaces;
using ClashGui.Services;
using DynamicData;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public ProxyProviderListViewModel(IProxyProviderService proxyProviderService)
    {
        proxyProviderService.List
            .Transform(d => new ProxyProviderViewModel(d) as IProxyProviderViewModel)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
        
        CheckCommand = ReactiveCommand.CreateFromTask<string>(async name =>
            await proxyProviderService.HealthCheckProxyProvider(name));
        UpdateCommand = ReactiveCommand.CreateFromTask<string>(async name =>
            await proxyProviderService.UpdateProxyProvider(name));
    }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<IProxyProviderViewModel>? ProxyProviders => _items;

    private ReadOnlyObservableCollection<IProxyProviderViewModel> _items;


    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }

}