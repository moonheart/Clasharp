using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
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

public partial class ProxiesListControl : ReactiveUserControl<ProxyListViewModel>
{
    public ProxiesListControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
        Dispatcher.UIThread.InvokeAsync(async () => await LoadProxyProviders(), DispatcherPriority.Background);
        Dispatcher.UIThread.InvokeAsync(async () => await LoadProxyGroups(), DispatcherPriority.Background);
    }

    private async Task LoadProxyProviders()
    {
        var proxyProviders = await GlobalConfigs.ClashControllerApi.GetProxyProviders();
        var providers = proxyProviders.Providers?.Values.Where(d =>
            d.VehicleType != VehicleType.Compatible && d.VehicleType != VehicleType.Unknown);
        if (DataContext is ProxyListViewModel proxyListViewModel && providers != null)
        {
            proxyListViewModel.ProxyProviders.Clear();
            foreach (var provider in providers)
            {
                proxyListViewModel.ProxyProviders.Add(provider);
            }
        }
    }

    private static string[] _notShownProxyGroups = new[] {"DIRECT", "GLOBAL"};
    private async Task LoadProxyGroups()
    {
        var proxyGroups = await GlobalConfigs.ClashControllerApi.GetProxyGroups();
        var proxyList = proxyGroups.Proxies?.Values;
        if (DataContext is ProxyListViewModel proxyListViewModel && proxyList != null)
        {
            proxyListViewModel.ProxyGropups.Clear();
            foreach (var proxyGroup in proxyList)
            {
                proxyListViewModel.ProxyGropups.Add(proxyGroup);
            }
        }
    }
}