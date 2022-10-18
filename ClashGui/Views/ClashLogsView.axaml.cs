using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Clash.Models.Logs;
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
        this.WhenActivated(disposable => { });
        AvaloniaXamlLoader.Load(this);
        Dispatcher.UIThread.InvokeAsync(async () => GetLogs(), DispatcherPriority.Background);
    }

    public async Task GetLogs()
    {
        if (DataContext is ClashLogsViewModel proxyListViewModel)
        {
            await foreach (var realtimeLog in GlobalConfigs.ClashControllerApi.GetRealtimeLogs())
            {
                var logEntry = JsonSerializer.Deserialize<LogEntry>(realtimeLog, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                });
                if (logEntry != null)
                {
                    proxyListViewModel.Logs.Insert(0, new LogEntryExt(logEntry));
                }
            }
        }
    }
}