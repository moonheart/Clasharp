using System;
using System.Collections.Generic;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyGroupListViewModel : ViewModelBase, IProxyGroupListViewModel
{
    public List<ProxyGroupExt> ProxyGroups => new List<ProxyGroupExt>
    {
        new ProxyGroupExt(new ProxyGroup()
        {
            All = new List<string>() {"asdf", "asdfasdf", "fgsdfg"},
            History = new List<ProxyHistory>() {new ProxyHistory() {Delay = 123, Time = DateTime.Now}},
            Name = "asdasd",
            Now = "asdfasdf",
            Type = "type",
            Udp = true
        })
    };
}