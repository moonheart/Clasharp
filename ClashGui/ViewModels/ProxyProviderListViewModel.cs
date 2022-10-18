using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Providers;
using ClashGui.Interfaces;
using ClashGui.Models.Providers;
using ClashGui.Models.Proxies;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public ProxyProviderListViewModel()
    {
        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(GetProxyGroups)
            .ToProperty(this, d => d.ProxyProviders, out _proxyProviders);
    }

    private readonly ObservableAsPropertyHelper<List<ProxyProviderExt>> _proxyProviders;
    public List<ProxyProviderExt> ProxyProviders => _proxyProviders.Value;

    private async Task<List<ProxyProviderExt>> GetProxyGroups(long _)
    {
        var providerData = await GlobalConfigs.ClashControllerApi.GetProxyProviders();
        return providerData.Providers?.Values.Where(d =>
                d.VehicleType != VehicleType.Compatible && d.VehicleType != VehicleType.Unknown)
            .Select(pp => new ProxyProviderExt(pp)).ToList() ?? new List<ProxyProviderExt>();
    }
}