using System.Collections.Generic;
using ClashGui.Models.Proxies;

namespace ClashGui.Interfaces;

public interface IProxyGroupViewModel
{
    string Name { get; }
    
    string Type { get;  }
    
    IEnumerable<SelectProxy> Proxies { get; }
    
    SelectProxy? SelectedProxy { get; set; }
}