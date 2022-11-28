using System.Collections.ObjectModel;
using System.Reactive;
using Clasharp.Clash.Models.Providers;
using Clasharp.Clash.Models.Rules;
using ReactiveUI;

namespace Clasharp.Interfaces;

public interface IProxyRulesListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<RuleInfo> Rules { get; }
    ReadOnlyObservableCollection<RuleProvider> Providers { get; }
    
    ReactiveCommand<string, Unit> UpdateCommand { get; }
}