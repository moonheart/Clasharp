using System;
using System.Collections.Generic;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Providers;
using ClashGui.Models.Proxies;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyProviderListViewModel : ViewModelBase, IProxyProviderListViewModel
{
    public List<ProxyProviderExt> ProxyProviders => new List<ProxyProviderExt>()
    {
        new ProxyProviderExt(new ProxyProvider()
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
        })
    };
}