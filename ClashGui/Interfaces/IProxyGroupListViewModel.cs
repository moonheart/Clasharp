using System.Collections.ObjectModel;

namespace ClashGui.Interfaces;

public interface IProxyGroupListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<IProxyGroupViewModel> ProxyGroupViewModels { get; }
}