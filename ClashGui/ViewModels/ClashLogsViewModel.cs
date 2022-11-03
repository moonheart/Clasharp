using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Logs;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Models.Logs;
using ClashGui.Utils;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ClashLogsViewModel : ViewModelBase, IClashLogsViewModel
{
    public override string Name => "Logs";
    private LogWatcher _logWatcher = new();
    private IClashCli _clashCli;
    public ClashLogsViewModel(IClashCli clashCli)
    {
        _clashCli = clashCli;
        LogsSource = new ObservableCollectionExtended<LogEntryExt>();
        LogsSource.ToObservableChangeSet()
            .Bind(out _logs)
            .Subscribe();

        MessageBus.Current.Listen<LogEntry>().Subscribe(d =>
        {
            if (LogsSource.Count >= 1000)
            {
                LogsSource.RemoveAt(LogsSource.Count - 1);
            }

            LogsSource.Insert(0, new LogEntryExt(d));
        });

        _clashCli.RunningObservable.Subscribe(d =>
        {
            if (d == RunningState.Started)
            {
                if (!string.IsNullOrWhiteSpace(_clashCli.Config.ExternalController))
                {
                    _logWatcher.Start(_clashCli.Config.ExternalController);
                }
            }
            else
            {
                _logWatcher.Stop();
            }
        });
    }

    public ObservableCollectionExtended<LogEntryExt> LogsSource { get; }
    private readonly ReadOnlyObservableCollection<LogEntryExt> _logs;
    public ReadOnlyObservableCollection<LogEntryExt> Logs => _logs;

    private class LogWatcher: Watcher
    {
        protected override async Task Watch(string uri, CancellationToken cancellationToken)
        {
            await foreach (var realtimeLog in GlobalConfigs.ClashControllerApi.GetRealtimeLogs(uri).WithCancellation(cancellationToken))
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