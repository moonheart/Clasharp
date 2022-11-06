using System.Collections.Generic;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Interfaces;
using ClashGui.Models.Proxies;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignProxyGroupViewModel : ViewModelBase, IProxyGroupViewModel
{
    public DesignProxyGroupViewModel()
    {
        SelectedProxy = new SelectProxy() {Group = "ssg", Proxy = "hk3"};
    }

    public string Name => "ssg";
    public ProxyGroupType Type => ProxyGroupType.Http;

    public IEnumerable<SelectProxy> Proxies => new[]
    {
        new SelectProxy() {Group = "ssg", Proxy = "hk1"},
        new SelectProxy() {Group = "ssg", Proxy = "hk2"},
        new SelectProxy() {Group = "ssg", Proxy = "hk3"}
    };

    public SelectProxy? SelectedProxy { get; set; }

    public bool Enabled => false;
}