using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Clasharp.Clash.Models.Providers;
using Clasharp.Interfaces;
using Clasharp.Models.Proxies;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels;

public sealed class ProxyProviderViewModel : ViewModelBase, IProxyProviderViewModel
{
    private readonly ProxyProvider _proxyProvider;

    public ProxyProviderViewModel(ProxyProvider proxyProvider)
    {
        _proxyProvider = proxyProvider;
        Proxies = _proxyProvider.Proxies.Select(pg => new ProxyGroupExt(pg)).ToList();
    }

    public bool IsLoading { [ObservableAsProperty] get; }

    public ProxyProvider ProxyProvider => _proxyProvider;

    public List<ProxyGroupExt> Proxies { get; }

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