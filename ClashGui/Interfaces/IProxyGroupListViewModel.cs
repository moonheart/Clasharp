using System.Collections.Generic;
using ClashGui.Models.Proxies;

namespace ClashGui.Interfaces;

public interface IProxyGroupListViewModel
{
    List<ProxyGroupExt> ProxyGroups { get; }
}