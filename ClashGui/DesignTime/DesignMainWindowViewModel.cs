using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime
{
    public class DesignMainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public DesignMainWindowViewModel()
        {
            ProxyListViewModel = new DesignProxyListViewModel();
            ClashLogsViewModel = new DesignClashLogsViewModel();
            ProxyRulesListViewModel = new DesignProxyRulesListViewModel();
            ConnectionsViewModel = new DesignConnectionsViewModel();
        }

        public IProxyListViewModel ProxyListViewModel { get; }

        public IClashLogsViewModel ClashLogsViewModel { get; }

        public IProxyRulesListViewModel ProxyRulesListViewModel { get; }

        public IConnectionsViewModel ConnectionsViewModel { get; }
    }
}