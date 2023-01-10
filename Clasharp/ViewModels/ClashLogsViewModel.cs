using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Clasharp.Cli;
using Clasharp.Interfaces;
using Clasharp.Models.Logs;
using Clasharp.Utils;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Clasharp.ViewModels;

public class ClashLogsViewModel : ViewModelBase, IClashLogsViewModel
{
    public override string Name => Resources.titleLogs;
    public ClashLogsViewModel(IClashCli clashCli)
    {
        LogsSource = new ObservableCollectionExtended<LogEntryExt>();
        LogsSource.ToObservableChangeSet()
            .Bind(out _logs)
            .Subscribe();

        clashCli.ConsoleLog.Subscribe(d =>
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