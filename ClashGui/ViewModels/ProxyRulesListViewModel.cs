using System.Collections.Generic;
using System.Reactive;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ClashGui.Interfaces;
using ClashGui.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => "Rules";

    public ProxyRulesListViewModel(IProxyRuleProviderService proxyRuleProviderService,
        IProxyRuleService proxyRuleService)
    {
        proxyRuleProviderService.Obj.ToPropertyEx(this, d => d.Providers);
        proxyRuleService.Obj.ToPropertyEx(this, d => d.Rules);

        UpdateCommand = ReactiveCommand.CreateFromTask<string>(async name =>
        {
            await GlobalConfigs.ClashControllerApi.UpdateRuleProvider(name);
        });
    }

    [ObservableAsProperty]
    public List<RuleInfo> Rules { get; }

    [ObservableAsProperty]
    public List<RuleProvider> Providers { get; }

    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}