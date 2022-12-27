using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
                this.FindControl<ExperimentalAcrylicBorder>("AcrylicBorder1").IsVisible = false;
                this.FindControl<ExperimentalAcrylicBorder>("AcrylicBorder2").IsVisible = false;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
    }
}