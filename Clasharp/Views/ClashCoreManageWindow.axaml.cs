using System.Runtime.InteropServices;
using Avalonia;
using Clasharp.ViewModels;

namespace Clasharp.Views;

public partial class ClashCoreManageWindow : WindowBase<ClashCoreManageViewModel>
{
    public ClashCoreManageWindow()
    {
        InitializeComponent();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            AcrylicBorder.IsVisible = false;
        }
    }
}