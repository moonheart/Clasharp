using Avalonia.ReactiveUI;
using ClashGui.ViewModels;

namespace ClashGui.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}