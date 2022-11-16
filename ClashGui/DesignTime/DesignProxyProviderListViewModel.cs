using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public ReadOnlyObservableCollection<IProxyProviderViewModel>? ProxyProviders { get; }

    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}