using System.Reactive;
using Clasharp.Cli;
using ReactiveUI;

namespace Clasharp.Interfaces;

public interface IClashInfoViewModel: IViewModelBase
{
    string Version { get; }
    
    string RealtimeSpeed { get; }
    
    ReactiveCommand<bool, Unit> ToggleClash { get; }
    
    bool IsRunning { get; }
    
    RunningState RunningState { get; }
}