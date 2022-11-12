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
            IDashboardViewModel dashboardViewModel, 
            ISettingsViewModel settingsViewModel, 
            IProfilesViewModel profilesViewModel, 
            IClashCli clashCli)
        {
            _clashCli = clashCli;
            ProxiesViewModel = proxiesViewModel;
            ClashLogsViewModel = clashLogsViewModel;
            ProxyRulesListViewModel = proxyRulesListViewModel;
            ConnectionsViewModel = connectionsViewModel;
            ClashInfoViewModel = clashInfoViewModel;
            DashboardViewModel = dashboardViewModel;
            SettingsViewModel = settingsViewModel;
            ProfilesViewModel = profilesViewModel;

            Selections = new ObservableCollection<IViewModelBase>();
            Selections.AddRange(new IViewModelBase[]
            {
                DashboardViewModel,
                ProxiesViewModel,
                ClashLogsViewModel,
                ProxyRulesListViewModel,
                ConnectionsViewModel,
                ProfilesViewModel,
                SettingsViewModel
            });
            CurrentViewModel = DashboardViewModel;
            // _ = _clashCli.Start();
        }

        [Reactive]
        public IViewModelBase CurrentViewModel { get; set; }

        public IProxiesViewModel ProxiesViewModel { get; }

        public IClashLogsViewModel ClashLogsViewModel { get; }

        public IProxyRulesListViewModel ProxyRulesListViewModel { get; }

        public IConnectionsViewModel ConnectionsViewModel { get; }
        public IClashInfoViewModel ClashInfoViewModel { get; }
        public ISettingsViewModel SettingsViewModel { get; }
        public IProfilesViewModel ProfilesViewModel { get; }
        public IDashboardViewModel DashboardViewModel { get; }
        public ObservableCollection<IViewModelBase> Selections { get; }
    }
}