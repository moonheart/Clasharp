using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;

namespace Clasharp.Views;

public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
{
    public DashboardView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}