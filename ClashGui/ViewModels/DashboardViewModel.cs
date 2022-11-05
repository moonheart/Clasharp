using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Models.Settings;
using ClashGui.Services;
using ClashGui.Utils;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;

namespace ClashGui.ViewModels;

public class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    public DashboardViewModel(IClashCli clashCli,
        ISettingsViewModel settingsViewModel,
        IRealtimeTrafficService realtimeTrafficService,
        IConnectionService connectionService)
    {
        Upload = "0 KB/s";
        Download = "0 KB/s";
        UploadTotal = "0 KB";
        DownloadTotal = "0 KB";
        
        StartClash = ReactiveCommand.CreateFromTask(async () => await clashCli.Start());
        StopClash = ReactiveCommand.CreateFromTask(async () => await clashCli.Stop());

        clashCli.RunningState
            .CombineLatest(clashCli.Config)
            .Subscribe(d =>
            {
                switch (settingsViewModel.SystemProxyMode)
                {
                    case SystemProxyMode.Clear:
                        ProxyUtils.UnsetSystemProxy();
                        break;
                    case SystemProxyMode.SetProxy:
                        if (d.First == RunningState.Started)
                        {
                            ProxyUtils.SetSystemProxy($"http://127.0.0.1:{d.Second.MixedPort ?? d.Second.Port}", "");
                        }
                        else if (d.First == RunningState.Stopped)
                        {
                            ProxyUtils.UnsetSystemProxy();
                        }

                        break;
                }
            });

        clashCli.RunningState.Subscribe(d =>
        {
            RunningState = d;
            IsStartingOrStopping = d is RunningState.Starting or RunningState.Stopping;
            IsStarted = d == RunningState.Started;
            IsStopped = d == RunningState.Stopped;
        });

        connectionService.Obj.Subscribe(d =>
        {
            DownloadTotal = d.DownloadTotal.ToHumanSize();
            UploadTotal = d.UploadTotal.ToHumanSize();
            ConnectionsCount = d.Connections.Count;
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
                TooltipLabelFormatter = point => point.Model.ToHumanSize(),
                Stroke = new SolidColorPaint(SKColors.Green) {StrokeThickness = 1},
                GeometrySize = 0
            },
            new LineSeries<long>()
            {
                Values = _upSpeeds,
                Name = "Upload",
                TooltipLabelFormatter = point => point.Model.ToHumanSize(),
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

    public override string Name => "Dashboard";
    public ReactiveCommand<Unit, Unit> StartClash { get; }
    public ReactiveCommand<Unit, Unit> StopClash { get; }

    [Reactive]
    public RunningState RunningState { get; set; }

    [Reactive]
    public bool IsStartingOrStopping { get; set; }

    [Reactive]
    public bool IsStarted { get; set; }

    [Reactive]
    public bool IsStopped { get; set; }

    private ObservableCollection<long> _upSpeeds = new();
    private ObservableCollection<long> _downSpeeds = new();

    [Reactive]
    public ISeries[] Series { get; set; }

    public Axis[] YAxes { get; set; } =
    {
        new()
        {
            MinLimit = 0,
            Labeler = d => ((long) d).ToHumanSize()
        }
    };
}