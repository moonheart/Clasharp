namespace ClashGui.Interfaces;

public interface IProxiesViewModel: IViewModelBase
{
    IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    IProxyProviderListViewModel ProxyProviderListViewModel { get; }

}