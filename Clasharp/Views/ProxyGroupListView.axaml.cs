using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;
using Clasharp.Models.Proxies;
using ReactiveUI;

namespace Clasharp.Views;

public partial class ProxyGroupListView : UserControlBase<ProxyGroupListViewModel>
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