using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyListViewModel : ViewModelBase, IProxyListViewModel
{
    public DesignProxyListViewModel()
    {
        ProxyGroupListViewModel = new DesignProxyGroupListViewModel();
        ProxyProviderListViewModel = new DesignProxyProviderListViewModel();
    }

    public IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    public IProxyProviderListViewModel ProxyProviderListViewModel { get; }


}