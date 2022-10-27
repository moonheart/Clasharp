using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Providers;
using ClashGui.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public ProxyProviderListViewModel()
    {
        var raw = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(GetProxyGroups)
            .Where(d => RawProxyProviders == null || !d.SequenceEqual(RawProxyProviders))
            .ToPropertyEx(this, d => d.RawProxyProviders);

        this.WhenAnyValue(d => d.RawProxyProviders)
            .WhereNotNull()
            .Select(d => d.Select(x => new ProxyProviderViewModel(x) as IProxyProviderViewModel).ToList())
            .ToProperty(this, d => d.ProxyProviders, out _proxyProviders);
    }

    public List<ProxyProvider> RawProxyProviders { [ObservableAsProperty] get; }

    private readonly ObservableAsPropertyHelper<List<IProxyProviderViewModel>?> _proxyProviders;
    public List<IProxyProviderViewModel>? ProxyProviders => _proxyProviders.Value;

    private async Task<List<ProxyProvider>?> GetProxyGroups(long _)
    {
        var providerData = await GlobalConfigs.ClashControllerApi.GetProxyProviders();
        return providerData?.Providers?.Values.Where(d =>
            d.VehicleType != VehicleType.Compatible && d.VehicleType != VehicleType.Unknown).ToList();
    }
}