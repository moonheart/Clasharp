using System.Reactive;
using Clasharp.Cli;
using Clasharp.Interfaces;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignClashInfoViewModel:ViewModelBase, IClashInfoViewModel
{
    public string Version => "v1111\nPremium";
    public string RealtimeSpeed => "↑ 12KB/s\n↓ 34KB/s";
    public ReactiveCommand<bool, Unit> ToggleClash { get; }
    public bool IsRunning { get; }
    public RunningState RunningState { get; }
}