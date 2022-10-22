using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Logs;
using ClashGui.Interfaces;
using ClashGui.Models.Logs;
using ClashGui.Utils;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ClashLogsViewModel : ViewModelBase, IClashLogsViewModel
{
    public ClashLogsViewModel()
    {
        LogsSource = new ObservableCollectionExtended<LogEntryExt>();
        LogsSource.ToObservableChangeSet()
            .Bind(out _logs)
            .Subscribe();

        var logWatcher = new LogWatcher();
        logWatcher.Start();

        MessageBus.Current.Listen<LogEntry>().Subscribe(d =>
        {
            if (LogsSource.Count >= 1000)
            {
                LogsSource.RemoveAt(LogsSource.Count - 1);
            }

            LogsSource.Insert(0, new LogEntryExt(d));
        });
    }

    public ObservableCollectionExtended<LogEntryExt> LogsSource { get; }
    private readonly ReadOnlyObservableCollection<LogEntryExt> _logs;
    public ReadOnlyObservableCollection<LogEntryExt> Logs => _logs;

    private class LogWatcher
    {
        public void Start()
        {
            Watch().ConfigureAwait(false);
        }

        private async Task Watch()
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
                MessageBus.Current.SendMessage(logEntry);
            }
        }
    }
}