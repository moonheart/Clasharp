using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

namespace Clasharp.Interfaces;

public interface IProxyProviderListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<IProxyProviderViewModel>? ProxyProviders { get; }
        
    ReactiveCommand<string, Unit> CheckCommand { get; }
    
    ReactiveCommand<string, Unit> UpdateCommand { get; }


}