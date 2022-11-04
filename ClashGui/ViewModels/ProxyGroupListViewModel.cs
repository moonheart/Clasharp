using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace ClashGui.ViewModels;

public class ProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    private static readonly string[] NotShownProxyGroups = {"DIRECT", "GLOBAL", "REJECT", "COMPATIBLE", "PASS"};

    private IClashCli _clashCli;
    public ProxyGroupListViewModel(IClashCli clashCli)
    {
        _clashCli = clashCli;
        var newData = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Where(d=>_clashCli.Running == RunningState.Started)
            .SelectMany(GetProxyGroups)
            .Where(items => ProxyGroups == null || !items.SequenceEqual(ProxyGroups, _proxyGroupComparer));
        
        _proxyGroups = newData
            .ToProperty(this, d => d.ProxyGroups);
        
        _proxyGroupViewModels = this.WhenAnyValue(x => x.ProxyGroups)
            .Where(d => d != null)
            .Select(d => d.Select(x => new ProxyGroupViewModel(x) as IProxyGroupViewModel).ToList())
            .ToProperty(this, d => d.ProxyGroupViewModels);
    }

    private readonly ObservableAsPropertyHelper<List<ProxyGroup>?> _proxyGroups;
    private List<ProxyGroup>? ProxyGroups => _proxyGroups.Value;

    private readonly ObservableAsPropertyHelper<List<IProxyGroupViewModel>> _proxyGroupViewModels;
    public List<IProxyGroupViewModel> ProxyGroupViewModels => _proxyGroupViewModels.Value;

    private async Task<List<ProxyGroup>> GetProxyGroups(long _)
    {
        var proxyData = await GlobalConfigs.ClashControllerApi.GetProxyGroups();
        return proxyData?.Proxies?.Values.Where(d => !NotShownProxyGroups.Contains(d.Name)).ToList() ??
               new List<ProxyGroup>();
    }

    private static ProxyGroupComparer _proxyGroupComparer = new();

    private class ProxyGroupComparer : IEqualityComparer<ProxyGroup>
    {
        public bool Equals(ProxyGroup? x, ProxyGroup? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.All.SequenceEqual(y.All)
                   && x.Name == y.Name
                   && x.Now == y.Now
                   && x.Type == y.Type
                   && x.Udp == y.Udp;
        }

        public int GetHashCode(ProxyGroup obj)
        {
            return HashCode.Combine(obj.All, obj.Name, obj.Now, obj.Type, obj.Udp);
        }
    }
}