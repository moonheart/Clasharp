using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using Clasharp.Clash.Models.Providers;
using Clasharp.Clash.Models.Rules;
using Clasharp.Interfaces;
using Clasharp.Services;
using DynamicData;
using ReactiveUI;

namespace Clasharp.ViewModels;

public class ProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => Resources.titleRules;

    public ProxyRulesListViewModel(IProxyRuleProviderService proxyRuleProviderService,
        IProxyRuleService proxyRuleService)
    {
        proxyRuleProviderService.List.ObserveOn(RxApp.MainThreadScheduler).Bind(out _ruleProviders).Subscribe();
        proxyRuleService.List.ObserveOn(RxApp.MainThreadScheduler).Bind(out _ruleInfos).Subscribe();

        UpdateCommand = ReactiveCommand.CreateFromTask<string>(async name =>
        {
            await proxyRuleService.UpdateRuleProvider(name);
        });
    }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<RuleInfo> Rules => _ruleInfos;

    private readonly ReadOnlyObservableCollection<RuleInfo> _ruleInfos;

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<RuleProvider> Providers => _ruleProviders;
    private readonly ReadOnlyObservableCollection<RuleProvider> _ruleProviders;

    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}