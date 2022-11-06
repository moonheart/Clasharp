using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProxyProviderViewModel : ViewModelBase, IProxyProviderViewModel
{
    private readonly ProxyProvider _proxyProvider;

    public ProxyProviderViewModel(ProxyProvider proxyProvider)
    {
        _proxyProvider = proxyProvider;

        CheckCommand = ReactiveCommand.CreateFromTask<string>(async name =>
            await GlobalConfigs.ClashControllerApi.HealthCheckProxyProvider(name));
        UpdateCommand = ReactiveCommand.CreateFromTask<string>(async name =>
            await GlobalConfigs.ClashControllerApi.UpdateProxyProvider(name));

        CheckCommand.IsExecuting.Merge(UpdateCommand.IsExecuting)
            .ToPropertyEx(this, d => d.IsLoading);
    }

    public bool IsLoading { [ObservableAsProperty] get; }

    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }
    public ProxyProvider ProxyProvider => _proxyProvider;

    public List<ProxyGroupExt> Proxies => _proxyProvider.Proxies.Select(pg => new ProxyGroupExt(pg)).ToList();

    protected bool Equals(ProxyProviderViewModel other)
    {
        return _proxyProvider.Equals(other._proxyProvider);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProxyProviderViewModel) obj);
    }

    public override int GetHashCode()
    {
        return _proxyProvider.GetHashCode();
    }
}