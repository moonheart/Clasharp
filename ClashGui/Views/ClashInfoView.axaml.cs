using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ClashGui.Views;

public partial class ClashInfoView : UserControl
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