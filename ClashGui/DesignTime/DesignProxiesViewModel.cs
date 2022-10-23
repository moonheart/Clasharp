using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxiesViewModel : ViewModelBase, IProxiesViewModel
{
    public DesignProxiesViewModel()
    {
        ProxyGroupListViewModel = new DesignProxyGroupListViewModel();
        ProxyProviderListViewModel = new DesignProxyProviderListViewModel();
    }

    public IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    public IProxyProviderListViewModel ProxyProviderListViewModel { get; }
}