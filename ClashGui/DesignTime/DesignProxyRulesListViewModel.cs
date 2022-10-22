using System;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public ReadOnlyObservableCollection<RuleInfo> Rules { get; } = new(new(new[]
        {new RuleInfo() {Type = "type", Payload = "paylaod", Proxy = "ssdfsdf"}}));

    public ReadOnlyObservableCollection<RuleProvider> Providers { get; } = new (new(new[]
    {
        new RuleProvider()
        {
            Behavior = "behaviour", Name = "name", RuleCount = 123, Type = "ssg", UpdatedAt = DateTime.Now,
            VehicleType = VehicleType.HTTP
        }
    }));
}