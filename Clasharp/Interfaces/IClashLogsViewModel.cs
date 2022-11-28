using System.Collections.ObjectModel;
using Clasharp.Models.Logs;

namespace Clasharp.Interfaces;

public interface IClashLogsViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<LogEntryExt> Logs { get; }

}