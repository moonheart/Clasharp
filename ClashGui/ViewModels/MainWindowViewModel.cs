using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Interfaces;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public MainWindowViewModel()
        {
            ProxiesViewModel = new ProxiesViewModel();
            ClashLogsViewModel = new ClashLogsViewModel();
            ProxyRulesListViewModel = new ProxyRulesListViewModel();
            ConnectionsViewModel = new ConnectionsViewModel();
            Selections = new ObservableCollection<IViewModelBase>();
            Selections.AddRange(new IViewModelBase[]
            {
                ProxiesViewModel,
                ClashLogsViewModel,
                ProxyRulesListViewModel,
                ConnectionsViewModel
            });
            CurrentViewModel = ProxiesViewModel;
        }

        [Reactive]
        public IViewModelBase CurrentViewModel { get; set; }

        public IProxiesViewModel ProxiesViewModel { get; }

        public IClashLogsViewModel ClashLogsViewModel { get; }

        public IProxyRulesListViewModel ProxyRulesListViewModel { get; }

        public IConnectionsViewModel ConnectionsViewModel { get; }
        public ObservableCollection<IViewModelBase> Selections { get; }
    }
}