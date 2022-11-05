using System;
using System.Reactive.Linq;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Services;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ClashInfoViewModel : ViewModelBase, IClashInfoViewModel
{
    private IClashCli _clashCli;

    public ClashInfoViewModel(IClashCli clashCli, IRealtimeTrafficService realtimeTrafficService)
    {
        _clashCli = clashCli;
        realtimeTrafficService.Obj.Subscribe(d =>
        {
            RealtimeSpeed = $"↑ {d.Up.ToHumanSize()}/s\n↓ {d.Down.ToHumanSize()}/s";
        });

        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Where(d => _clashCli.Running == RunningState.Started)
            .SelectMany(async _ => await GlobalConfigs.ClashControllerApi.GetClashVersion())
            .WhereNotNull()
            .Select(d => $"{d.Version}\n{(d.Premium ? "Premium" : "")}")
            .ToProperty(this, d => d.Version);
    }

    [ObservableAsProperty]
    public string Version { get; }

    [Reactive]
    public string RealtimeSpeed { get; set; }

}