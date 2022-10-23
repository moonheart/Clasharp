using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ClashGui.Interfaces;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => "Rules";
    public ProxyRulesListViewModel()
    {
        RuleInfoSource = new ObservableCollectionExtended<RuleInfo>();
        RuleInfoSource.ToObservableChangeSet()
            .Bind(out _rules)
            .Subscribe();

        RuleProviderSource = new ObservableCollectionExtended<RuleProvider>();
        RuleProviderSource.ToObservableChangeSet()
            .Bind(out _providers)
            .Subscribe();

        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(async _ => await GlobalConfigs.ClashControllerApi.GetRules())
            .Select(d=>d.Rules ?? new List<RuleInfo>())
            .Where(d=> !RuleInfoSource.SequenceEqual(d))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(d =>
            {
                RuleInfoSource.Clear();
                RuleInfoSource.AddRange(d);
            });

        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(async _ => await GlobalConfigs.ClashControllerApi.GetRuleProviders())
            .Select(d=>d.Providers?.Values.ToList() ?? new List<RuleProvider>())
            .Where(d=> !RuleProviderSource.SequenceEqual(d))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(d =>
            {
                RuleProviderSource.Clear();
                RuleProviderSource.AddRange(d);
            });
    }

    private readonly ReadOnlyObservableCollection<RuleInfo> _rules;
    public ReadOnlyObservableCollection<RuleInfo> Rules => _rules;
    public ObservableCollectionExtended<RuleInfo> RuleInfoSource { get; }


    private ReadOnlyObservableCollection<RuleProvider> _providers;
    public ReadOnlyObservableCollection<RuleProvider> Providers => _providers;
    public ObservableCollectionExtended<RuleProvider> RuleProviderSource { get; }
}