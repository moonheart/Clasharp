using System.Collections.Generic;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Models.Proxies;

namespace Clasharp.Interfaces;

public interface IProxyGroupViewModel: IViewModelBase
{
    string Name { get; }
    
    ProxyGroupType Type { get;  }
    
    IEnumerable<SelectProxy> Proxies { get; }
    
    SelectProxy? SelectedProxy { get; set; }
}