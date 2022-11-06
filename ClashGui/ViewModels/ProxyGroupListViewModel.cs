using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ClashGui.Interfaces;
using ClashGui.Services;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public ProxyGroupListViewModel(IProxyGroupService proxyGroupService)
    {
        proxyGroupService.Obj
            .Select(d => d.Select(x => new ProxyGroupViewModel(x, proxyGroupService.SelectProxy) as IProxyGroupViewModel).ToList())
            .ToPropertyEx(this, d => d.ProxyGroupViewModels);
    }

    [ObservableAsProperty]
    public List<IProxyGroupViewModel> ProxyGroupViewModels { get; }
}