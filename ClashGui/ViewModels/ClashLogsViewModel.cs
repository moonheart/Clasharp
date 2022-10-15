using System;
using System.Collections.ObjectModel;
using ClashGui.Models.Logs;

namespace ClashGui.ViewModels;

public class ClashLogsViewModel : ViewModelBase
{
    public ClashLogsViewModel()
    {
        Logs = new ObservableCollection<LogEntry>();
    }

    public ObservableCollection<LogEntry> Logs { get; set; }
}