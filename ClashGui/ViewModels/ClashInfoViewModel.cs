using System;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Models.Settings;
using ClashGui.Services;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ClashInfoViewModel : ViewModelBase, IClashInfoViewModel
{
    public ClashInfoViewModel(
        IRealtimeTrafficService realtimeTrafficService,
        ISettingsViewModel settingsViewModel,
        IVersionService versionService,
        IClashCli clashCli)
    {
        Version = "Unknown";
        
        realtimeTrafficService.Obj.Select(d => $"↑ {d.Up.ToHumanSize()}/s\n↓ {d.Down.ToHumanSize()}/s")
            .ToPropertyEx(this, d => d.RealtimeSpeed);

        versionService.Obj.Select(d => $"{d.Version}\n{(d.Premium ? "Premium" : "")}")
            .ToPropertyEx(this, d => d.Version);

        ToggleClash = ReactiveCommand.CreateFromTask<bool>(async b =>
        {
            if (b)
            {
                await clashCli.Start();
            }
            else
            {
                await clashCli.Stop();
            }
        });
        
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
    }

    [ObservableAsProperty]
    public string Version { get; }

    [ObservableAsProperty]
    public string RealtimeSpeed { get; }

    public ReactiveCommand<bool, Unit> ToggleClash { get; }
}