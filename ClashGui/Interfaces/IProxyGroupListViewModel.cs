using System.Collections.Generic;
using ClashGui.Models.Proxies;

namespace ClashGui.Interfaces;

public interface IProxyGroupListViewModel: IViewModelBase
{
    List<IProxyGroupViewModel> ProxyGroupViewModels { get; }
}