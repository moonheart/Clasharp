using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;

namespace ClashGui.Interfaces;

public interface IProxyRulesListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<RuleInfo> Rules { get; }
    ReadOnlyObservableCollection<RuleProvider> Providers { get; }
}