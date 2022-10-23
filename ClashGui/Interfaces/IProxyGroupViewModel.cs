using System.Collections.Generic;
using ClashGui.Models.Proxies;

namespace ClashGui.Interfaces;

public interface IProxyGroupViewModel: IViewModelBase
{
    string Name { get; }
    
    ProxyGroupType Type { get;  }
    
    IEnumerable<SelectProxy> Proxies { get; }
    
    SelectProxy? SelectedProxy { get; set; }
}