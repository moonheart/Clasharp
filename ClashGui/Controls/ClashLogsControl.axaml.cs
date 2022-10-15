using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Models.Logs;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Controls;

public partial class ClashLogsControl : ReactiveUserControl<ClashLogsViewModel>
{
    public ClashLogsControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
        Dispatcher.UIThread.InvokeAsync(async () => GetLogs(), DispatcherPriority.Background);
    }

    public async Task GetLogs()
    {
        if (DataContext is ClashLogsViewModel proxyListViewModel)
        {
            Trace.WriteLine("GetRealtimeLogs");
            await foreach (var realtimeLog in GlobalConfigs.ClashControllerApi.GetRealtimeLogs())
            {
                Trace.WriteLine(realtimeLog);
                var logEntry = JsonSerializer.Deserialize<LogEntry>(realtimeLog, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                if (logEntry != null)
                {
                    proxyListViewModel.Logs.Add(logEntry);
                }
            }
        }
    }
}