using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.ViewModels;

namespace ClashGui.Views;

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