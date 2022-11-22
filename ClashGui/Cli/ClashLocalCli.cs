using System.Threading.Tasks;
using ClashGui.Common;
using ClashGui.Models.Settings;
using ClashGui.Services;

namespace ClashGui.Cli;

public class ClashLocalCli : ClashCliBase
{
    private ClashWrapper? _clashWrapper;

    public ClashLocalCli(IClashApiFactory clashApiFactory, IProfilesService profilesService, AppSettings appSettings)
        : base(clashApiFactory, profilesService, appSettings)
    {
        _runningState.OnNext(Cli.RunningState.Stopped);
    }

    protected override async Task DoStart(string configPath)
    {
        _clashWrapper?.Stop();
        _clashWrapper = new ClashWrapper(new ClashLaunchInfo()
        {
            ConfigPath = configPath, ExecutablePath = GlobalConfigs.ClashExe, WorkDir = GlobalConfigs.ProgramHome
        })
        {
            OnNewLog = CliLogProcessor
        };
        _clashWrapper.Start();
    }

    protected override Task DoStop()
    {
        _clashWrapper?.Stop();
        return Task.CompletedTask;
    }
}