using System.Collections.ObjectModel;
using Clasharp.Interfaces;
using Clasharp.ViewModels;

namespace Clasharp.DesignTime;

public class DesignProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public ReadOnlyObservableCollection<ProxyGroupModel> ProxyGroupViewModels { get; }
}