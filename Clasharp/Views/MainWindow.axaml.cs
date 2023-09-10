using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Input;
using Clasharp.ViewModels;

namespace Clasharp.Views
{
    public partial class MainWindow : WindowBase<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                AcrylicBorder1.IsVisible = false;
                AcrylicBorder2.IsVisible = false;
            }
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
        }
    }
}