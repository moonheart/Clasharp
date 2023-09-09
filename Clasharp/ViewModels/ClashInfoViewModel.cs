using System;
using System.Reactive;
using System.Reactive.Linq;
using Clasharp.Cli;
using Clasharp.Interfaces;
using Clasharp.Services;
using Clasharp.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RunningState = Clasharp.Cli.Generated.RunningState;

namespace Clasharp.ViewModels;

public class ClashInfoViewModel : ViewModelBase, IClashInfoViewModel
{
    public ClashInfoViewModel(
        IRealtimeTrafficService realtimeTrafficService,
        ISettingsViewModel settingsViewModel,
        IVersionService versionService,
        IClashCli clashCli)
    {
        Version = "Unknown";
        RealtimeSpeed = "Unknown";
        RunningState = RunningState.Stopped;

        realtimeTrafficService.Obj.Select(d => $"↑ {d.Up.ToHumanSize()}/s\n↓ {d.Down.ToHumanSize()}/s")
            .ToPropertyEx(this, d => d.RealtimeSpeed);

        versionService.Obj.Select(d => $"{(d.Meta ? "Meta" : "")} {d.Version}")
            .ToPropertyEx(this, d => d.Version);

        ToggleClash = ReactiveCommand.CreateFromTask<bool>(async b =>
        {
            try
            {
                if (b)
                {
                    await clashCli.Start();
                }
                else
                {
                    await clashCli.Stop();
                }
            }
            catch (Exception e)
            {
                await ShowError.Handle((e, false));
            }
        });

        clashCli.RunningState.Select(d => d == Cli.Generated.RunningState.Started).ToPropertyEx(this, d => d.IsRunning);
        clashCli.RunningState.ToPropertyEx(this, d => d.RunningState);
    }

    [ObservableAsProperty]
    public string Version { get; }

    [ObservableAsProperty]
    public string RealtimeSpeed { get; }

    public ReactiveCommand<bool, Unit> ToggleClash { get; }

    [ObservableAsProperty]
    public bool IsRunning { get; }

    [ObservableAsProperty]
    public Cli.Generated.RunningState RunningState { get; }
}