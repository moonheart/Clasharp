using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Providers;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProxyProviderListViewModel: IViewModelBase
{
    List<IProxyProviderViewModel>? ProxyProviders { get; }

}