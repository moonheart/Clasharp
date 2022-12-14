using System.Threading.Tasks;
using Clasharp.Models.Settings;
using Clasharp.Services;
using Clasharp.Common;

namespace Clasharp.Cli;

public class ClashLocalCli : ClashCliBase
{
    private ClashWrapper? _clashWrapper;

    public ClashLocalCli(IClashApiFactory clashApiFactory, IProfilesService profilesService, AppSettings appSettings)
        : base(clashApiFactory, profilesService, appSettings)
    {
    }

    protected override async Task DoStart(string configPath)
    {
        _clashWrapper?.Stop();
        _clashWrapper = new ClashWrapper(new ClashLaunchInfo
        {
            ConfigPath = configPath, ExecutablePath = await GetClashExePath.Exec(), WorkDir = GlobalConfigs.ProgramHome
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