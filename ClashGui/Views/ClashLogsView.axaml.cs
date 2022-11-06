using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Models.Logs;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Views;

public partial class ClashLogsView : ReactiveUserControl<ClashLogsViewModel>
{
    public ClashLogsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}