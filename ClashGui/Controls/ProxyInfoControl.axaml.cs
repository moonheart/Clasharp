using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.ViewModels;

namespace ClashGui.Controls;

public partial class ProxyInfoControl : ReactiveUserControl<ProxyInfoViewModel>
{
    public ProxyInfoControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}