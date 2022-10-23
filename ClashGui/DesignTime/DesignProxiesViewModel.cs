using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxiesViewModel : ViewModelBase, IProxiesViewModel
{
    public override string Name => "Proxies";

    public DesignProxiesViewModel()
    {
        ProxyGroupListViewModel = new DesignProxyGroupListViewModel();
        ProxyProviderListViewModel = new DesignProxyProviderListViewModel();
    }

    public IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    public IProxyProviderListViewModel ProxyProviderListViewModel { get; }
}