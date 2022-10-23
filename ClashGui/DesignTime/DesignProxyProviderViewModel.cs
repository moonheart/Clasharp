using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProxyProviderViewModel:ViewModelBase, IProxyProviderViewModel
{
    public bool IsLoading { get; set; }
    public ReactiveCommand<string, Unit> CheckCommand { get; }
    public ReactiveCommand<string, Unit> UpdateCommand { get; }

    public ProxyProvider ProxyProvider => new ProxyProvider()
    {
        Name = "ssg", Proxies = new List<ProxyGroup>()
        {
            new ProxyGroup()
            {
                All = new List<string>() {"asdf", "asdfasdf", "fgsdfg"},
                History = new List<ProxyHistory>() {new ProxyHistory() {Delay = 123, Time = DateTime.Now}},
                Name = "asdasd",
                Now = "asdfasdf",
                Type = ProxyGroupType.Trojan,
                Udp = true
            }
        },
        Type = "trojan", VehicleType = VehicleType.HTTP, UpdatedAt = DateTime.Now
    };

    public List<ProxyGroupExt> Proxies => ProxyProvider.Proxies.Select(pg => new ProxyGroupExt(pg)).ToList();
}