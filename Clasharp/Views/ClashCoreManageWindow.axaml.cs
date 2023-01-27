using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Clasharp.ViewModels;

namespace Clasharp.Views;

public partial class ClashCoreManageWindow : WindowBase<ClashCoreManageViewModel>
{
    public ClashCoreManageWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            AcrylicBorder.IsVisible = false;
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}