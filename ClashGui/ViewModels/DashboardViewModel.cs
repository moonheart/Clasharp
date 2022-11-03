using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ClashGui.Clash.Models;
using ClashGui.Cli;
using ClashGui.Cli.ClashConfigs;
using ClashGui.Interfaces;
using ClashGui.Utils;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private IClashCli _clashCli;

    public DashboardViewModel(IClashCli clashCli)
    {
        _clashCli = clashCli;

        StartClash = ReactiveCommand.CreateFromTask(async () =>
        {
            var rawConfig = await _clashCli.Start();
            ProxyUtils.SetSystemProxy($"http://127.0.0.1:{rawConfig.MixedPort ?? rawConfig.Port}", "");
            _rawConfig = rawConfig;
        });

        StopClash = ReactiveCommand.CreateFromTask(async () =>
        {
            await _clashCli.Stop();
            ProxyUtils.UnsetSystemProxy();
        });

        _clashCli.RunningObservable.BindTo(this, d => d.RunningState);

        _clashCli.RunningObservable.Subscribe(d =>
        {
            IsStartingOrStopping = d == RunningState.Starting || d == RunningState.Stopping;
            IsStarted = d == RunningState.Started;
            IsStopped = d == RunningState.Stopped;
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
            },
            new LineSeries<long>()
            {
                Values = _upSpeeds,
                Name = "Upload",
                TooltipLabelFormatter = point => point.Model.ToHumanSize(),
                GeometryFill = null,
                GeometryStroke = null,
            }
        };

        this.WhenAnyValue(d => d._rawConfig)
            .WhereNotNull()
            .Subscribe(d => ExternalController = d.ExternalController);

        MessageBus.Current.Listen<TrafficEntry>().Subscribe(d =>
        {
            if (_downSpeeds.Count >= 60) _downSpeeds.RemoveAt(0);
            _downSpeeds.Add(d.Down);
            if (_upSpeeds.Count >= 60) _upSpeeds.RemoveAt(0);
            _upSpeeds.Add(d.Up);
        });
    }

    [Reactive]
    private RawConfig _rawConfig { get; set; }

    [Reactive]
    public string? ExternalController { get; set; }

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

    private ObservableCollection<long> _upSpeeds = new ObservableCollection<long>();
    private ObservableCollection<long> _downSpeeds = new ObservableCollection<long>();

    [Reactive]
    public ISeries[] Series { get; set; }

    public Axis[] YAxes { get; set; } = new Axis[]
    {
        new()
        {
            MinLimit = 0,
            Labeler = d => ((long) d).ToHumanSize() 
        }
    };
}