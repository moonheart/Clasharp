using System.Reactive;
using System.Threading.Tasks;
using ClashGui.Cli;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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

    Axis[] YAxes { get; set; }
    ISeries[] Series { get; set; }

    public string? ExternalController { get; }

    public string? Upload { get; }
    public string? Download { get; }
    public string? UploadTotal { get; }
    public string? DownloadTotal { get; }
    public int ConnectionsCount { get; }
}