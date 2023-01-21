using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Clasharp.Clash.Models;
using Clasharp.Cli;
using Clasharp.Utils;

namespace Clasharp.Services;

public class RealtimeTrafficService : IRealtimeTrafficService
{
    public IObservable<TrafficEntry> Obj => _trafficEntry;
    public bool EnableAutoFresh { get; set; }

    private readonly ReplaySubject<TrafficEntry> _trafficEntry = new();

    public RealtimeTrafficService(IClashCli clashCli, IClashApiFactory clashApiFactory)
    {
        var trafficWatcher = new TrafficWatcher(_trafficEntry, clashApiFactory);
        clashCli.RunningState
            .CombineLatest(clashCli.Config)
            .Subscribe(d =>
        {
            if (d.First == Cli.Generated.RunningState.Started && !string.IsNullOrWhiteSpace(d.Second.ExternalController))
            {
                trafficWatcher.Start(d.Second.ExternalController);
            }
            else
            {
                trafficWatcher.Stop();
            }
        });
    }

    private sealed class TrafficWatcher : Watcher<TrafficEntry>
    {
        private readonly IClashApiFactory _clashApiFactory;
        public TrafficWatcher(ReplaySubject<TrafficEntry> replaySubject, IClashApiFactory clashApiFactory) : base(replaySubject)
        {
            _clashApiFactory = clashApiFactory;
        }

        protected override async Task Watch(string uri, CancellationToken cancellationToken)
        {
            await foreach (var realtimeTraffic in _clashApiFactory.Get().GetRealtimeTraffic(uri)
                               .WithCancellation(cancellationToken))
            {
                var trafficEntry = JsonSerializer.Deserialize<TrafficEntry>(realtimeTraffic, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                });
                ReplaySubject.OnNext(trafficEntry ?? new TrafficEntry());
            }
        }
    }
}