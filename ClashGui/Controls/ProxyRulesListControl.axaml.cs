using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Clash.Models.Providers;
using ClashGui.ViewModels;

namespace ClashGui.Controls;

public partial class ProxyRulesListControl : ReactiveUserControl<ProxyRulesListViewModel>, IDisposable
{
    private Timer _loadRulesTimer;
    private Timer _loadRuleProvidersTimer;

    public ProxyRulesListControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _loadRulesTimer = new Timer(_ => LoadRules().ConfigureAwait(false).GetAwaiter().GetResult(),
            null, TimeSpan.Zero, TimeSpan.FromSeconds(100));
        _loadRuleProvidersTimer = new Timer(_ => LoadRuleProviders().ConfigureAwait(false).GetAwaiter().GetResult(),
            null, TimeSpan.Zero, TimeSpan.FromSeconds(100));
    }

    private async Task LoadRules()
    {
        var data = await GlobalConfigs.ClashControllerApi.GetRules();
        var rules = data.Rules;
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (ViewModel != null && rules != null)
            {
                ViewModel.Rules.Clear();
                foreach (var rule in rules)
                {
                    ViewModel.Rules.Add(rule);
                }
            }
        }, DispatcherPriority.Background);
    }

    private async Task LoadRuleProviders()
    {
        var data = await GlobalConfigs.ClashControllerApi.GetRuleProviders();
        var providers = data.Providers?.Values;
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (ViewModel != null && providers != null)
            {
                ViewModel.Providers.Clear();
                foreach (var provider in providers)
                {
                    ViewModel.Providers.Add(provider);
                }
            }
        }, DispatcherPriority.Background);
    }

    public void Dispose()
    {
        _loadRulesTimer.Dispose();
    }
}