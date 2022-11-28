using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;

namespace Clasharp.Views;

public partial class ClashInfoView : ReactiveUserControl<ClashInfoViewModel>
{
    public ClashInfoView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}