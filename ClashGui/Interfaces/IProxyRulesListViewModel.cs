using System.Collections.Generic;
using System.Reactive;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Common.ApiModels.Rules;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProxyRulesListViewModel: IViewModelBase
{
    List<RuleInfo> Rules { get; }
    List<RuleProvider> Providers { get; }
    
    ReactiveCommand<string, Unit> UpdateCommand { get; }
}