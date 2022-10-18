using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ReactiveUI;
using Splat;

namespace ClashGui.ViewModels;

public class ProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    private static readonly string[] NotShownProxyGroups = {"DIRECT", "GLOBAL", "REJECT"};

    public ProxyGroupListViewModel()
    {
        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(GetProxyGroups)
            .ToProperty(this, d => d.ProxyGroups, out _proxyGroups);
    }

    private readonly ObservableAsPropertyHelper<List<ProxyGroupExt>> _proxyGroups;
    public List<ProxyGroupExt> ProxyGroups => _proxyGroups.Value;

    private async Task<List<ProxyGroupExt>> GetProxyGroups(long _)
    {
        var proxyData = await GlobalConfigs.ClashControllerApi.GetProxyGroups();
        return proxyData.Proxies?.Values.Where(d => !NotShownProxyGroups.Contains(d.Name))
            .Select(pg => new ProxyGroupExt(pg)
            ).ToList() ?? new List<ProxyGroupExt>();
    }
}