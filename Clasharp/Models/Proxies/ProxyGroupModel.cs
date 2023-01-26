using System;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Models.Proxies;
using DynamicData;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels;

public class ProxyGroupModel : ReactiveObject
{
    private readonly ProxyGroup _proxyGroup;

    public ProxyGroupModel(ProxyGroup proxyGroup, Func<string, string, Task> setProxy)
    {
        _proxyGroup = proxyGroup;
        SelectedProxy = _proxyGroup.Now == null
            ? null
            : new SelectProxy {Group = _proxyGroup.Name, Proxy = _proxyGroup.Now};
        Enabled = _proxyGroup.Type == ProxyGroupType.Selector;
        Expanded = true;
        var proxies = _proxyGroup.All.Select(p => new SelectProxy {Group = _proxyGroup.Name, Proxy = p}).ToList();
        Proxies.AddRange(proxies);

        var selectedProxyB = SelectedProxy;
        this.WhenAnyValue(d => d.FilterString)
            .Skip(1)
            .Throttle(TimeSpan.FromMilliseconds(200))
            .Subscribe(filter =>
            {
                if (string.IsNullOrEmpty(filter))
                {
                    foreach (var selectProxy in proxies.Where(selectProxy => !Proxies.Contains(selectProxy)))
                    {
                        Proxies.Add(selectProxy);
                    }

                    SelectedProxy = selectedProxyB;
                    return;
                }

                foreach (var selectProxy in proxies
                             .Where(d => !d.Proxy.Contains(filter, StringComparison.OrdinalIgnoreCase))
                             .Where(Proxies.Contains))
                {
                    Proxies.Remove(selectProxy);
                }

                SelectedProxy = selectedProxyB != null && Proxies.Contains(selectedProxyB) ? selectedProxyB : null;

            });

        this.WhenAnyValue(d => d.SelectedProxy)
            .WhereNotNull()
            .Skip(1)
            .Subscribe(d =>
            {
                setProxy(d.Group, d.Proxy)
                    .ConfigureAwait(false).GetAwaiter().GetResult();
            });
    }

    public string Name => _proxyGroup.Name;
    public ProxyGroupType Type => _proxyGroup.Type;

    public ObservableCollection<SelectProxy> Proxies { get; set; } = new();

    [Reactive]
    public SelectProxy? SelectedProxy { get; set; }

    [Reactive]
    public string FilterString { get; set; }
    
    [Reactive]
    public bool Expanded { get; set; }

    public bool Enabled { get; }

    public override bool Equals(object? obj)
    {
        if (obj is ProxyGroupModel other)
        {
            return _proxyGroup.All.SequenceEqual(other._proxyGroup.All)
                   && Equals(_proxyGroup.Name, other._proxyGroup.Name)
                   && Equals(_proxyGroup.Type, other._proxyGroup.Type)
                   && Equals(_proxyGroup.Now, other._proxyGroup.Now);
        }

        return false;
    }
}