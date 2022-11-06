using System.Reactive;
using System.Threading.Tasks;
using ClashGui.Cli;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IDashboardViewModel : IViewModelBase
{
    Axis[] YAxes { get; set; }
    ISeries[] Series { get; set; }

    public string? ExternalController { get; }

    public string? Upload { get; }
    public string? Download { get; }
    public string? UploadTotal { get; }
    public string? DownloadTotal { get; }
    public int ConnectionsCount { get; }
}