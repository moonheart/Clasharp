using System;
using System.Collections.Generic;
using System.Reactive;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Common.ApiModels.Rules;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => "Rules";

    public List<RuleInfo> Rules { get; } = new()
    {
        new RuleInfo() {Type = "type", Payload = "paylaod", Proxy = "ssdfsdf"}
    };

    public List<RuleProvider> Providers { get; } = new()
    {
        new RuleProvider()
        {
            Behavior = "behaviour", Name = "name", RuleCount = 123, Type = "ssg", UpdatedAt = DateTime.Now,
            VehicleType = VehicleType.HTTP
        }
    };

    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}