using System.Threading.Tasks;
using Clasharp.Common;
using Clasharp.Models.ServiceMode;
using Clasharp.Utils.PlatformOperations;

namespace Clasharp.Utils;

public class CoreServiceHelper
{
    private readonly GetCoreServicePath _getCoreServicePath = new();
    private readonly InstallService _installService = new();
    private readonly UninstallService _uninstallService = new();
    private readonly StartService _startService = new();
    private readonly StopService _stopService = new();
    private readonly GetServiceStatus _getServiceStatus = new();

    public async Task Install()
    {
        var path = await _getCoreServicePath.Exec();
        await _installService.Exec(GlobalConfigs.CoreServiceName, GlobalConfigs.CoreServiceDesc, path);
    }

    public async Task Uninstall()
    {
        await _uninstallService.Exec(GlobalConfigs.CoreServiceName);
    }

    public async Task<ServiceStatus> Status()
    {
        return await _getServiceStatus.Exec(GlobalConfigs.CoreServiceName);
    }

    public async Task Start()
    {
        await _startService.Exec(GlobalConfigs.CoreServiceName);
    }

    public async Task Stop()
    {
        await _stopService.Exec(GlobalConfigs.CoreServiceName);
    }
}