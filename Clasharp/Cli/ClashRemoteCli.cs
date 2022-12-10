using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clasharp.Clash;
using Clasharp.Models.ServiceMode;
using Clasharp.Models.Settings;
using Clasharp.Services;
using Clasharp.Utils;
using Clasharp.Common;
using Refit;

namespace Clasharp.Cli;

public interface IRemoteClash
{
    [Post("/start_clash")]
    Task StartClash([Body(buffered: true)] ClashLaunchInfo clashLaunchInfo);

    [Post("/stop_clash")]
    Task StopClash();

    [Get("/is_running")]
    Task<bool> IsRunning();

    IAsyncEnumerable<string> GetRealtimeLogs() =>
        new Streamer($"http://localhost:{GlobalConfigs.ClashServicePort}", "/logs");
}

public class ClashRemoteCli : ClashCliBase
{
    private IRemoteClash _remoteClash;
    private CoreServiceHelper _coreServiceHelper;

    public ClashRemoteCli(IClashApiFactory clashApiFactory, IProfilesService profilesService, AppSettings appSettings,
        IRemoteClash remoteClash,
        CoreServiceHelper coreServiceHelper)
        : base(clashApiFactory, profilesService, appSettings)
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

        await _remoteClash.StartClash(new ClashLaunchInfo
        {
            ExecutablePath = await GetClashExePath.Exec(),
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