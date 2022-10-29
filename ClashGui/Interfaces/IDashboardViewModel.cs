using System.Reactive;
using System.Threading.Tasks;
using ClashGui.Cli;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IDashboardViewModel : IViewModelBase
{
    ReactiveCommand<Unit, Unit> StartClash { get; }
    ReactiveCommand<Unit, Unit> StopClash { get; }

    RunningState RunningState { get; }
    
    bool IsStartingOrStopping { get; }
    
    bool IsStarted { get; }
    bool IsStopped { get; }
}