using System.Collections.ObjectModel;
using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public ReadOnlyObservableCollection<IProxyGroupViewModel> ProxyGroupViewModels { get; }
}