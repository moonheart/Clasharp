using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ClashGui.Clash.Models;
using ClashGui.Interfaces;
using ClashGui.Utils;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ClashInfoViewModel : ViewModelBase, IClashInfoViewModel
{
    public ClashInfoViewModel()
    {
        MessageBus.Current.Listen<TrafficEntry>().Subscribe(d =>
        {
            RealtimeSpeed = $"↑ {d.Up.ToHumanSize()}/s\n↓ {d.Down.ToHumanSize()}/s";
        });

        _version = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(async _ => await GlobalConfigs.ClashControllerApi.GetClashVersion())
            .Select(d => $"{d.Version}\n{(d.Premium ? "Premium" : "")}")
            .ToProperty(this, d => d.Version);

        new TrafficWatcher().Start();
    }

    // public string Version { [ObservableAsProperty] get; }

    private readonly ObservableAsPropertyHelper<string> _version;
    public string Version => _version.Value;


    [Reactive]
    public string RealtimeSpeed { get; set; }

    private class TrafficWatcher
    {
        public void Start()
        {
            Watch().ConfigureAwait(false);
        }

        private async Task Watch()
        {
            await foreach (var realtimeTraffic in GlobalConfigs.ClashControllerApi.GetRealtimeTraffic())
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