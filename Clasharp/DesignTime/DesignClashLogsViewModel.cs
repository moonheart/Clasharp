using System.Collections.ObjectModel;
using Clasharp.Clash.Models.Logs;
using Clasharp.Interfaces;
using Clasharp.Models.Logs;
using Clasharp.ViewModels;

namespace Clasharp.DesignTime;

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