using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Logs;
using ClashGui.Interfaces;
using ClashGui.Models.Logs;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignClashLogsViewModel : ViewModelBase, IClashLogsViewModel
{
    public override string Name => "Logs";
    public DesignClashLogsViewModel()
    {
        Logs = new ReadOnlyObservableCollection<LogEntryExt>(new(new[]
        {
            new LogEntryExt(new LogEntry(LogLevel.INFO, "contenlkjsalfjaslkdjfl")),
            new LogEntryExt(new LogEntry(LogLevel.INFO, "contenlkjsalfjaslkdjfl")),
            new LogEntryExt(new LogEntry(LogLevel.INFO, "contenlkjsalfjaslkdjfl")),
            new LogEntryExt(new LogEntry(LogLevel.INFO, "contenlkjsalfjaslkdjfl")),
            new LogEntryExt(new LogEntry(LogLevel.INFO, "contenlkjsalfjaslkdjfl")),
        }));
    }

    public ReadOnlyObservableCollection<LogEntryExt> Logs { get; set; }
}