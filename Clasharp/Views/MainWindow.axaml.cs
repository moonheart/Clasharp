using Avalonia;
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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
    }
}