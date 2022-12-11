using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.Views;

public partial class ProxyProviderListView : UserControlBase<ProxyProviderListViewModel>
{
    public ProxyProviderListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
    }
}