using System.Runtime.InteropServices;
using Avalonia;
using Clasharp.ViewModels;

namespace Clasharp.Views
{
    public partial class MainWindow : WindowBase<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                AcrylicBorder1.IsVisible = false;
                AcrylicBorder2.IsVisible = false;
            }
        }
    }
}