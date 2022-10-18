using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;

namespace ClashGui.Interfaces;

public interface IProxyRulesListViewModel
{
    ObservableCollection<RuleInfo> Rules { get; }
    ObservableCollection<RuleProvider> Providers { get; }
}