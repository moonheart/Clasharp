using System.Reactive;
using ClashGui.Cli;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IClashInfoViewModel: IViewModelBase
{
    string Version { get; }
    
    string RealtimeSpeed { get; }
    
    ReactiveCommand<bool, Unit> ToggleClash { get; }
    
    bool IsRunning { get; }
    
    RunningState RunningState { get; }
}