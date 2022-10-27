using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Cli;
using ClashGui.Interfaces;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private IClashCli _clashCli;
        public MainWindowViewModel(
            IProxiesViewModel proxiesViewModel,
            IClashLogsViewModel clashLogsViewModel,
            IProxyRulesListViewModel proxyRulesListViewModel,
            IConnectionsViewModel connectionsViewModel,
            IClashInfoViewModel clashInfoViewModel, 
            IClashCli clashCli)
        {
            ProxiesViewModel = proxiesViewModel;
            ClashLogsViewModel = clashLogsViewModel;
            ProxyRulesListViewModel = proxyRulesListViewModel;
            ConnectionsViewModel = connectionsViewModel;
            ClashInfoViewModel = clashInfoViewModel;
            _clashCli = clashCli;

            Selections = new ObservableCollection<IViewModelBase>();
            Selections.AddRange(new IViewModelBase[]
            {
                ProxiesViewModel,
                ClashLogsViewModel,
                ProxyRulesListViewModel,
                ConnectionsViewModel
            });
            CurrentViewModel = ProxiesViewModel;
            // _ = _clashCli.Start();
        }

        [Reactive]
        public IViewModelBase CurrentViewModel { get; set; }

        public IProxiesViewModel ProxiesViewModel { get; }

        public IClashLogsViewModel ClashLogsViewModel { get; }

        public IProxyRulesListViewModel ProxyRulesListViewModel { get; }

        public IConnectionsViewModel ConnectionsViewModel { get; }
        public IClashInfoViewModel ClashInfoViewModel { get; }
        public ObservableCollection<IViewModelBase> Selections { get; }
    }
}