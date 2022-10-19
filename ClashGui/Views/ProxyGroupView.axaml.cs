using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.ViewModels;

namespace ClashGui.Views;

public partial class ProxyGroupView : ReactiveUserControl<ProxyGroupViewModel>
{
    public ProxyGroupView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}