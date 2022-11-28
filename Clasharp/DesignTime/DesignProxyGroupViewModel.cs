using System.Collections.Generic;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Interfaces;
using Clasharp.Models.Proxies;
using Clasharp.ViewModels;

namespace Clasharp.DesignTime;

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