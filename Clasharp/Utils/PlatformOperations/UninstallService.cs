using System;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class UninstallService : PlatformSpecificOperation<string, int>
{
    private readonly RunEvaluatedCommand _evaluatedCommand = new();
    private readonly StopService _stopService = new();

    /// <summary>
    /// Uninstall service
    /// </summary>
    /// <param name="serviceName">service name</param>
    /// <returns></returns>
    public override Task<int> Exec(string serviceName)
    {
        return base.Exec(serviceName);
    }

    protected override async Task<int> DoForWindows(string serviceName)
    {
        var result = await _evaluatedCommand.Exec($"sc stop {serviceName} && sc delete {serviceName}");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to uninstall service {serviceName}: {result.StdOut}");
        }

        return 0;
    }

    protected override async Task<int> DoForLinux(string serviceName)
    {
        var result = await _evaluatedCommand.Exec($"systemctl stop {serviceName} && rm /etc/systemd/system/{serviceName}.service && systemctl daemon-reload");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to uninstall service {serviceName}: {result.StdOut}");
        }

        return 0;
    }
}