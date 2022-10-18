namespace ClashGui.Interfaces;

public interface IProxyListViewModel
{
    IProxyGroupListViewModel ProxyGroupListViewModel { get; }
    IProxyProviderListViewModel ProxyProviderListViewModel { get; }

}