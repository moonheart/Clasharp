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
    public Axis[] YAxes { get; set; }

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<double>
        {
            Values = new double[] {2, 1, 3, 5, 3, 4, 6},
            Fill = null
        }
    };

    public string? ExternalController { get; } = "127.0.0.1:8908";
    public string? Upload { get; } = "12KB/s";
    public string? Download { get; } = "12KB/s";
    public string? UploadTotal { get; } = "12KB";
    public string? DownloadTotal { get; } = "12KB";
    public int ConnectionsCount { get; } = 10;
}