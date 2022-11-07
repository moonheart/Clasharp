using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClashGui.Clash;
using ClashGui.Clash.Models.Logs;
using ClashGui.Common;
using ClashGui.Services;
using Refit;
using LogLevel = ClashGui.Clash.Models.Logs.LogLevel;

namespace ClashGui.Cli;

public interface IRemoteClash
{
    [Post("/start_clash")]
    Task StartClash([Body] ClashLaunchInfo clashLaunchInfo);

    [Post("/stop_clash")]
    Task StopClash();

    IAsyncEnumerable<string> GetRealtimeLogs() =>
        new Streamer($"http://localhost:{GlobalConfigs.ClashServicePort}", "/logs");
}

public class ClashRemoteCli : ClashCliBase
{
    private IRemoteClash _remoteClash;

    public ClashRemoteCli(IClashApiFactory clashApiFactory, IRemoteClash remoteClash)
    {
        _clashApiFactory = clashApiFactory;
        _remoteClash = remoteClash;
        _runningState.OnNext(Cli.RunningState.Stopped);
    }


    private CancellationTokenSource? _cancellationTokenSource;

    protected override async Task DoStart()
    {
        await _remoteClash.StartClash(new ClashLaunchInfo()
        {
            ExecutablePath = GlobalConfigs.ClashExe,
            WorkDir = GlobalConfigs.ProgramHome
        });

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {
            await foreach (var realtimeLog in _remoteClash.GetRealtimeLogs()
                               .WithCancellation(_cancellationTokenSource.Token))
            {
                if (string.IsNullOrEmpty(realtimeLog)) return;
                var match = _logRegex.Match(realtimeLog);
                if (!match.Success) match = _logMetaRegex.Match(realtimeLog);
                _consoleLog.OnNext(match.Success
                    ? new LogEntry(_levelsMap[match.Groups["level"].Value], match.Groups["payload"].Value)
                    : new LogEntry(LogLevel.INFO, realtimeLog));
            }
        });
    }

    protected override async Task DoStop()
    {
        _cancellationTokenSource?.Cancel();
        await _remoteClash.StopClash();
    }
}