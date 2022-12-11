using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.Views;

public partial class ProxiesView : UserControlBase<ProxiesViewModel>
{
    public ProxiesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}