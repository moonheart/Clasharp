using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;

namespace Clasharp.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
    }
}