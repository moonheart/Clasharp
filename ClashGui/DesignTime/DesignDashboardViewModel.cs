using System.Reactive;
using System.Threading.Tasks;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignDashboardViewModel:ViewModelBase, IDashboardViewModel
{
    public override string Name => "Dashboard";
    public ReactiveCommand<Unit, Unit> StartClash { get; }
    public ReactiveCommand<Unit, Unit> StopClash { get; }
    public RunningState RunningState { get; }
}