using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Media;
using Avalonia.Themes.Fluent;
using Clasharp.Interfaces;
using Clasharp.Models.Settings;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public MainWindowViewModel(
            IProxiesViewModel proxiesViewModel,
            IClashLogsViewModel clashLogsViewModel,
            IProxyRulesListViewModel proxyRulesListViewModel,
            IConnectionsViewModel connectionsViewModel,
            IClashInfoViewModel clashInfoViewModel,
            IDashboardViewModel dashboardViewModel,
            ISettingsViewModel settingsViewModel,
            IProfilesViewModel profilesViewModel,
            AppSettings appSettings)
        {
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

            appSettings.WhenAnyValue(d => d.ThemeMode)
                .Select(d => d == FluentThemeMode.Dark ? Colors.Black : Colors.White)
                .ToPropertyEx(this, d => d.TintColor);
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

        [ObservableAsProperty]
        public Color TintColor { get; }
    }
}