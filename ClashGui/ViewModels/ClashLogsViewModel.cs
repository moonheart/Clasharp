using System;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Logs;
using ClashGui.Models.Logs;

namespace ClashGui.ViewModels;

public class ClashLogsViewModel : ViewModelBase
{
    public ClashLogsViewModel()
    {
        Logs = new ObservableCollection<LogEntryExt>();
    }

    public ObservableCollection<LogEntryExt> Logs { get; set; }
}