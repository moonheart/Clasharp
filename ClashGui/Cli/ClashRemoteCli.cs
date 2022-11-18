using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClashGui.Clash;
using ClashGui.Clash.Models.Logs;
using ClashGui.Common;
using ClashGui.Models.ServiceMode;
using ClashGui.Services;
using ClashGui.Utils;
using Refit;
using LogLevel = ClashGui.Clash.Models.Logs.LogLevel;

namespace ClashGui.Cli;

public interface IRemoteClash
{
    [Post("/start_clash")]
    Task StartClash([Body(buffered: true)] ClashLaunchInfo clashLaunchInfo);

    [Post("/stop_clash")]
    Task StopClash();

    IAsyncEnumerable<string> GetRealtimeLogs() =>
        new Streamer($"http://localhost:{GlobalConfigs.ClashServicePort}", "/logs");
}

public class ClashRemoteCli : ClashCliBase
{
    private IRemoteClash _remoteClash;
    private CoreServiceHelper _coreServiceHelper;

    public ClashRemoteCli(IClashApiFactory clashApiFactory, IProfilesService profilesService, IRemoteClash remoteClash,
        CoreServiceHelper coreServiceHelper)
    : base(clashApiFactory, profilesService)
    {
        _clashApiFactory = clashApiFactory;
        _remoteClash = remoteClash;
        _coreServiceHelper = coreServiceHelper;
        _runningState.OnNext(Cli.RunningState.Stopped);
    }


    private CancellationTokenSource? _cancellationTokenSource;

    protected override async Task DoStart(string configPath)
    {
        var status = await _coreServiceHelper.Status();
        if (status != ServiceStatus.Running)
        {
            _runningState.OnNext(Cli.RunningState.Stopped);
            throw new Exception("Core service not installed or running");
        }

        await _remoteClash.StartClash(new ClashLaunchInfo()
        {
            ExecutablePath = GlobalConfigs.ClashExe,
            WorkDir = GlobalConfigs.ProgramHome,
            ConfigPath = configPath
        });

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {
            await foreach (var realtimeLog in _remoteClash.GetRealtimeLogs()
                               .WithCancellation(_cancellationTokenSource.Token))
            {
                CliLogProcessor(realtimeLog);
            }
        });
    }

    protected override async Task DoStop()
    {
        _cancellationTokenSource?.Cancel();
        await _remoteClash.StopClash();
    }
}