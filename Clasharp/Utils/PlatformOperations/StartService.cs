using System;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class StartService : PlatformSpecificOperation<string, int>
{
    protected override async Task<int> DoForWindows(string serviceName)
    {
        var result = await new RunEvaluatedCommand().Exec("sc", $"start {serviceName}");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to start service {serviceName}: {result.StdOut}");
        }

        return 0;
    }

    protected override async Task<int> DoForLinux(string serviceName)
    {
        var result = await new RunEvaluatedCommand().Exec("systemctl", $"start {serviceName}");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to start service {serviceName}: {result.StdOut}");
        }

        return 0;
    }
}