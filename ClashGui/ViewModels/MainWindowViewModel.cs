using ClashGui.Interfaces;

namespace ClashGui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public MainWindowViewModel()
        {
            ProxyListViewModel = new ProxyViewModel();
            ClashLogsViewModel = new ClashLogsViewModel();
            ProxyRulesListViewModel = new ProxyRulesListViewModel();
            ConnectionsViewModel = new ConnectionsViewModel();
        }

        public IProxyListViewModel ProxyListViewModel { get; }

        public IClashLogsViewModel ClashLogsViewModel { get; }

        public IProxyRulesListViewModel ProxyRulesListViewModel { get; }

        public IConnectionsViewModel ConnectionsViewModel { get; }
    }
}