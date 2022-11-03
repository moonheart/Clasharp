using System.Reactive;
using System.Threading.Tasks;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignDashboardViewModel : ViewModelBase, IDashboardViewModel
{
    public override string Name => "Dashboard";
    public ReactiveCommand<Unit, Unit> StartClash { get; }
    public ReactiveCommand<Unit, Unit> StopClash { get; }
    public RunningState RunningState { get; }
    public bool IsStartingOrStopping { get; } = false;
    public bool IsStarted { get; } = true;
    public bool IsStopped { get; } = true;
    public Axis[] YAxes { get; set; }

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<double>
        {
            Values = new double[] {2, 1, 3, 5, 3, 4, 6},
            Fill = null
        }
    };

    public string? ExternalController { get; }
}