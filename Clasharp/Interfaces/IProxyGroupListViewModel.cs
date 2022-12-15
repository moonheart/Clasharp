using System.Collections.ObjectModel;
using Clasharp.ViewModels;

namespace Clasharp.Interfaces;

public interface IProxyGroupListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<ProxyGroupModel> ProxyGroupViewModels { get; }
}