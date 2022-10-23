using System.Collections.ObjectModel;

namespace ClashGui.Interfaces;

public interface IMainWindowViewModel: IViewModelBase
{
    IViewModelBase CurrentViewModel { get; set; }
    
    IProxiesViewModel ProxiesViewModel { get; }

    IClashLogsViewModel ClashLogsViewModel { get; }

    IProxyRulesListViewModel ProxyRulesListViewModel { get; }

    IConnectionsViewModel ConnectionsViewModel { get; }
    
    ObservableCollection<IViewModelBase> Selections { get; }
}