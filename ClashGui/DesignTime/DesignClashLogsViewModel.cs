using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Logs;
using ClashGui.Interfaces;
using ClashGui.Models.Logs;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignClashLogsViewModel : ViewModelBase, IClashLogsViewModel
{
    public DesignClashLogsViewModel()
    {
        Logs = new ObservableCollection<LogEntryExt>(new []
        {
            new LogEntryExt(new LogEntry(){Type = LogLevel.INFO, Payload = "contenlkjsalfjaslkdjfl"}),
            new LogEntryExt(new LogEntry(){Type = LogLevel.INFO, Payload = "contenlkjsalfjaslkdjfl"}),
            new LogEntryExt(new LogEntry(){Type = LogLevel.INFO, Payload = "contenlkjsalfjaslkdjfl"}),
            new LogEntryExt(new LogEntry(){Type = LogLevel.INFO, Payload = "contenlkjsalfjaslkdjfl"}),
            new LogEntryExt(new LogEntry(){Type = LogLevel.INFO, Payload = "contenlkjsalfjaslkdjfl"}),
        });
    }

    public ObservableCollection<LogEntryExt> Logs { get; set; }
}