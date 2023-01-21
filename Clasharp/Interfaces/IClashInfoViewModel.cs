using System.Reactive;
using ReactiveUI;
using RunningState = Clasharp.Cli.Generated.RunningState;

namespace Clasharp.Interfaces;

public interface IClashInfoViewModel: IViewModelBase
{
    string Version { get; }
    
    string RealtimeSpeed { get; }
    
    ReactiveCommand<bool, Unit> ToggleClash { get; }
    
    bool IsRunning { get; }
    
    RunningState RunningState { get; }
}