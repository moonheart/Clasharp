using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Clash.Models.Providers;
using ClashGui.Models;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Controls;

public partial class ProxiesListControl : ReactiveUserControl<ProxyListViewModel>, IDisposable
{
    public ProxiesListControl()
    {
        InitializeComponent();
    }

    private Timer _loadProxyProvidersTimer;
    private Timer _loadProxyGroupsTimer;

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);

        _loadProxyProvidersTimer = new Timer(_ => LoadProxyProviders().ConfigureAwait(false).GetAwaiter().GetResult(),
            null, TimeSpan.Zero, TimeSpan.FromSeconds(100));
        _loadProxyGroupsTimer = new Timer(_ => LoadProxyGroups().ConfigureAwait(false).GetAwaiter().GetResult(),
            null, TimeSpan.Zero, TimeSpan.FromSeconds(100));
    }

    private async Task LoadProxyProviders()
    {
        var proxyProviders = await GlobalConfigs.ClashControllerApi.GetProxyProviders();
        var providers = proxyProviders.Providers?.Values.Where(d =>
            d.VehicleType != VehicleType.Compatible && d.VehicleType != VehicleType.Unknown);
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (DataContext is ProxyListViewModel proxyListViewModel && providers != null)
            {
                proxyListViewModel.ProxyProviders.Clear();
                foreach (var provider in providers)
                {
                    proxyListViewModel.ProxyProviders.Add(provider);
                }
            }
        }, DispatcherPriority.Background);
    }

    private static readonly string[] NotShownProxyGroups = {"DIRECT", "GLOBAL", "REJECT"};

    private async Task LoadProxyGroups()
    {
        var proxyGroups = await GlobalConfigs.ClashControllerApi.GetProxyGroups();
        var proxyList = proxyGroups.Proxies?.Values.Where(d => !NotShownProxyGroups.Contains(d.Name));
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (DataContext is ProxyListViewModel proxyListViewModel && proxyList != null)
            {
                proxyListViewModel.ProxyGropups.Clear();
                foreach (var proxyGroup in proxyList)
                {
                    proxyListViewModel.ProxyGropups.Add(proxyGroup);
                }
            }
        }, DispatcherPriority.Background);
    }

    public void Dispose()
    {
        _loadProxyProvidersTimer.Dispose();
        _loadProxyGroupsTimer.Dispose();
    }
}