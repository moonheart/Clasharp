using Clasharp.Interfaces;
using Clasharp.ViewModels;

namespace Clasharp.DesignTime;

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