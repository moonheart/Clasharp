using System;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Logs;
using ClashGui.Interfaces;
using ClashGui.Models.Logs;

namespace ClashGui.ViewModels;

public class ClashLogsViewModel : ViewModelBase, IClashLogsViewModel
{
    public ClashLogsViewModel()
    {
        Logs = new ObservableCollection<LogEntryExt>();
    }

    public ObservableCollection<LogEntryExt> Logs { get; set; }
}