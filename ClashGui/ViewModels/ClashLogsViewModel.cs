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
    public ClashLogsViewModel()
    {
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
    }

    public ObservableCollectionExtended<LogEntryExt> LogsSource { get; }
    private readonly ReadOnlyObservableCollection<LogEntryExt> _logs;
    public ReadOnlyObservableCollection<LogEntryExt> Logs => _logs;
}