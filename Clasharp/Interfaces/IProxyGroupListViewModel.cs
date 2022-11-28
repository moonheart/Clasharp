using System.Collections.ObjectModel;

namespace Clasharp.Interfaces;

public interface IProxyGroupListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<IProxyGroupViewModel> ProxyGroupViewModels { get; }
}