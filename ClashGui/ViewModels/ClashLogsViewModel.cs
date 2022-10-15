using System;
using System.Collections.ObjectModel;
using ClashGui.Models.Logs;

namespace ClashGui.ViewModels;

public class ClashLogsViewModel : ViewModelBase
{
    public ClashLogsViewModel()
    {
        Logs = new ObservableCollection<LogEntry>();
        Logs.Add(new LogEntry(){Type = "INFO", Payload = "testtestettesttestettesttestettesttestet"});
    }

    public ObservableCollection<LogEntry> Logs { get; set; }
}