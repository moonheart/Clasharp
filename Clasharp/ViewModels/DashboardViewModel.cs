using System;
using System.Collections.ObjectModel;
using System.Linq;
using Clasharp.Cli;
using Clasharp.Interfaces;
using Clasharp.Services;
using Clasharp.Utils;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;

namespace Clasharp.ViewModels;

public class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    public DashboardViewModel(IClashCli clashCli,
        IRealtimeTrafficService realtimeTrafficService,
        IConnectionService connectionService)
    {
        Upload = "0 KB/s";
        Download = "0 KB/s";
        UploadTotal = "0 KB";
        DownloadTotal = "0 KB";
      
        connectionService.Obj.Subscribe(d =>
        {
            DownloadTotal = d.DownloadTotal.ToHumanSize();
            UploadTotal = d.UploadTotal.ToHumanSize();
            ConnectionsCount = d.Connections?.Count ?? 0;
        });

        _downSpeeds.AddRange(Enumerable.Repeat<long>(0, 60));
        _upSpeeds.AddRange(Enumerable.Repeat<long>(0, 60));
        Series = new ISeries[]
        {
            new LineSeries<long>()
            {
                Values = _downSpeeds,
                Name = "Download",
                GeometryFill = null,
                GeometryStroke = null,
                XToolTipLabelFormatter = point => point.Model.ToHumanSize(),
                YToolTipLabelFormatter = point => point.Model.ToHumanSize(),
                Stroke = new SolidColorPaint(SKColors.Green) {StrokeThickness = 1},
                GeometrySize = 0
            },
            new LineSeries<long>()
            {
                Values = _upSpeeds,
                Name = "Upload",
                XToolTipLabelFormatter = point => point.Model.ToHumanSize(),
                YToolTipLabelFormatter = point => point.Model.ToHumanSize(),
                GeometryFill = null,
                GeometryStroke = null,
                Stroke = new SolidColorPaint(SKColors.Red) {StrokeThickness = 1},
                GeometrySize = 0
            }
        };

        clashCli.Config.Subscribe(d => ExternalController = d.ExternalController);

        realtimeTrafficService.Obj.Subscribe(d =>
        {
            if (_downSpeeds.Count >= 60) _downSpeeds.RemoveAt(0);
            _downSpeeds.Add(d.Down);
            if (_upSpeeds.Count >= 60) _upSpeeds.RemoveAt(0);
            _upSpeeds.Add(d.Up);
            Download = $"{d.Down.ToHumanSize()}/s";
            Upload = $"{d.Up.ToHumanSize()}/s";
        });
    }

    [Reactive]
    public string? ExternalController { get; set; }

    [Reactive]
    public string? Upload { get; set; }

    [Reactive]
    public string? Download { get; set; }

    [Reactive]
    public string? UploadTotal { get; set; }

    [Reactive]
    public string? DownloadTotal { get; set; }

    [Reactive]
    public int ConnectionsCount { get; set; }

    public override string Name => Resources.titleDashboard;

    private readonly ObservableCollection<long> _upSpeeds = new();
    private readonly ObservableCollection<long> _downSpeeds = new();


    [Reactive]
    public ISeries[] Series { get; set; }

    public Axis[] XAxes { get; set; } = {new()
    {
        Labeler = d => ""
    }};
    public Axis[] YAxes { get; set; } =
    {
        new()
        {
            MinLimit = 0,
            Labeler = d => ((long) d).ToHumanSize(),
            TextSize = 12,
        }
    };
}