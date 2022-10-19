using System;
using System.Collections.Generic;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public List<IProxyGroupViewModel> ProxyGroupViewModels => new()
    {
        new DesignProxyGroupViewModel()
    };
}