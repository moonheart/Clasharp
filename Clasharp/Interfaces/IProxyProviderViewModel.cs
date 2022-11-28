using System.Collections.Generic;
using System.Reactive;
using Clasharp.Clash.Models.Providers;
using Clasharp.Models.Proxies;
using ReactiveUI;

namespace Clasharp.Interfaces;

public interface IProxyProviderViewModel:IViewModelBase
{
    bool IsLoading { get; }

    ProxyProvider ProxyProvider { get; }
    
    List<ProxyGroupExt> Proxies { get; }
    
}