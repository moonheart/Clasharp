using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ClashGui.Clash.Models;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Utils;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ClashInfoViewModel : ViewModelBase, IClashInfoViewModel
{
    private IClashCli _clashCli;
    private TrafficWatcher _trafficWatcher = new();

    public ClashInfoViewModel(IClashCli clashCli)
    {
        _clashCli = clashCli;
        MessageBus.Current.Listen<TrafficEntry>().Subscribe(d =>
        {
            RealtimeSpeed = $"↑ {d.Up.ToHumanSize()}/s\n↓ {d.Down.ToHumanSize()}/s";
        });

        _version = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Where(d => _clashCli.Running == RunningState.Started)
            .SelectMany(async _ => await GlobalConfigs.ClashControllerApi.GetClashVersion())
            .WhereNotNull()
            .Select(d => $"{d.Version}\n{(d.Premium ? "Premium" : "")}")
            .ToProperty(this, d => d.Version);

        clashCli.RunningObservable.Subscribe(d =>
        {
            if (d == RunningState.Started)
            {
                if (!string.IsNullOrWhiteSpace(_clashCli.Config.ExternalController))
                {
                    _trafficWatcher.Start(_clashCli.Config.ExternalController);
                }
            }
            else
            {
                _trafficWatcher.Stop();
            }
        });
    }

    // public string Version { [ObservableAsProperty] get; }

    private readonly ObservableAsPropertyHelper<string> _version;
    public string Version => _version.Value;


    [Reactive]
    public string RealtimeSpeed { get; set; }

    private class TrafficWatcher : Watcher
    {
        protected override async Task Watch(string uri, CancellationToken cancellationToken)
        {
            await foreach (var realtimeTraffic in GlobalConfigs.ClashControllerApi.GetRealtimeTraffic(uri)
                               .WithCancellation(cancellationToken))
            {
                var logEntry = JsonSerializer.Deserialize<TrafficEntry>(realtimeTraffic, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                });
                MessageBus.Current.SendMessage(logEntry);
            }
        }
    }
}