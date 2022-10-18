namespace ClashGui.Interfaces;

public interface IMainWindowViewModel
{
    public IProxyListViewModel ProxyListViewModel { get; }

    public IClashLogsViewModel ClashLogsViewModel { get; }

    public IProxyRulesListViewModel ProxyRulesListViewModel { get; }

    public IConnectionsViewModel ConnectionsViewModel { get; }
}