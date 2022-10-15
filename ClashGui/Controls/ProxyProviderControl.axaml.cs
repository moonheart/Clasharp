using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Controls;

public partial class ProxyProviderControl : ReactiveUserControl<ProxyProviderViewModel>
{
    public ProxyProviderControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}