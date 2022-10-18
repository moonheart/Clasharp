using System.Collections.ObjectModel;
using ClashGui.Models.Logs;

namespace ClashGui.Interfaces;

public interface IClashLogsViewModel
{
    ObservableCollection<LogEntryExt> Logs { get; }

}