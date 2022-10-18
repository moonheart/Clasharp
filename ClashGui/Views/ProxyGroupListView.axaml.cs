using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.Models.Proxies;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Views;

public partial class ProxyGroupListView : ReactiveUserControl<ProxyGroupListViewModel>
{
    public ProxyGroupListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var proxy = e.AddedItems[0] as SelectProxy;
        if (proxy != null)
        {
            Debug.WriteLine("SelectingItemsControl_OnSelectionChanged");
            // GlobalConfigs.ClashControllerApi.SelectProxy(proxy.Group, new UpdateProxyRequest {Name = proxy.Proxy});
        }
    }
}