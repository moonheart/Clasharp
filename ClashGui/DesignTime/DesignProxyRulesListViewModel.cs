using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => "Rules";

    public ReadOnlyObservableCollection<RuleInfo> Rules { get; }

    public ReadOnlyObservableCollection<RuleProvider> Providers { get; } 

    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}