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
        AvaloniaXamlLoader.Load(this);
    }
}