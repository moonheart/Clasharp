using System.Collections.Generic;
using System.Reactive;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProxyRulesListViewModel: IViewModelBase
{
    List<RuleInfo> Rules { get; }
    List<RuleProvider> Providers { get; }
    
    ReactiveCommand<string, Unit> UpdateCommand { get; }
}