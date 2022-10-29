using System.ComponentModel;
using Avalonia.Controls;
using ClashGui.Cli;

namespace ClashGui.Views
{
    public partial class MainWindow : Window
    {
        public IClashCli? ClashCli;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ClashCli?.Stop();
            base.OnClosing(e);
        }
    }
}