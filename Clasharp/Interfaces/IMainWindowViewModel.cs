using System.Collections.ObjectModel;
using Avalonia.Media;

namespace Clasharp.Interfaces;

public interface IMainWindowViewModel: IViewModelBase
{
    IViewModelBase CurrentViewModel { get; set; }
    
    IProxiesViewModel ProxiesViewModel { get; }

    IClashLogsViewModel ClashLogsViewModel { get; }

    IProxyRulesListViewModel ProxyRulesListViewModel { get; }

    IConnectionsViewModel ConnectionsViewModel { get; }
    IClashInfoViewModel ClashInfoViewModel { get; }
    ISettingsViewModel SettingsViewModel { get; }
    IProfilesViewModel ProfilesViewModel { get; }
    
    ObservableCollection<IViewModelBase> Selections { get; }
    
    Color TintColor { get; }
}