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
using Grpc.Net.Client;
using Grpc.Core;

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
    private readonly CoreService.CoreServiceClient _remoteClash;
    private readonly CoreServiceHelper _coreServiceHelper;

    public ClashRemoteCli(IClashApiFactory clashApiFactory, IProfilesService profilesService, AppSettings appSettings,
        CoreServiceHelper coreServiceHelper)
        : base(clashApiFactory, profilesService, appSettings)
    {
        ClashApiFactory = clashApiFactory;
        var channel = GrpcChannel.ForAddress($"http://localhost:{GlobalConfigs.ClashServicePort}/");
        _remoteClash = new CoreService.CoreServiceClient(channel);
        _coreServiceHelper = coreServiceHelper;
    }

    private CancellationTokenSource? _cancellationTokenSource;

    protected override async Task DoStart(string configPath, bool useSystemCore)
    {
        var status = await _coreServiceHelper.Status();
        if (status != ServiceStatus.Running)
        {
            _runningState.OnNext(Generated.RunningState.Stopped);
            throw new InvalidOperationException("Core service not installed or running");
        }

        await _remoteClash.StartClashAsync(new StartClashRequest()
        {
            ExecutablePath = await GetClashExePath.Exec(useSystemCore),
            WorkDir = GlobalConfigs.ProgramHome,
            ConfigPath = configPath
        });

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        _ = Task.Run(async () =>
        {
            var logs = _remoteClash.Logs(new GetRealtimeLogs());

            await foreach (var log in logs.ResponseStream.ReadAllAsync())
            {
                CliLogProcessor(logs.ResponseStream.Current.Message);
            }
        });
    }

    protected override async Task DoStop()
    {
        _cancellationTokenSource?.Cancel();
        await _remoteClash.StopClashAsync(new StopClashRequest());
    }
}