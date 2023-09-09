using System.Collections.ObjectModel;
using System.Reactive;
using Clasharp.Clash.Models.Providers;
using Clasharp.Clash.Models.Rules;
using Clasharp.Interfaces;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => "Rules";

    public ReadOnlyObservableCollection<RuleInfo> Rules { get; } = new(new ObservableCollection<RuleInfo>());

    public ReadOnlyObservableCollection<RuleProvider> Providers { get; } = new(new ObservableCollection<RuleProvider>());

    public ReactiveCommand<string, Unit> UpdateCommand { get; } = ReactiveCommand.Create((string _) => { });
}