using System.Collections.ObjectModel;
using System.Reactive;
using Clasharp.Interfaces;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public ReadOnlyObservableCollection<IProxyProviderViewModel>? ProxyProviders { get; }

    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}