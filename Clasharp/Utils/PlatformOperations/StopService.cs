using System;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class StopService : PlatformSpecificOperation<string, int>
{
    protected override async Task<int> DoForWindows(string serviceName)
    {
        var result = await new RunEvaluatedCommand().Exec("sc", $"stop {serviceName}");
        // exit code 1062: service is not started
        if (result.ExitCode != 1062 && result.ExitCode != 0)
        {
            throw new Exception($"Failed to stop service {serviceName}: {result.StdOut}");
        }

        return 0;
    }

    protected override async Task<int> DoForLinux(string serviceName)
    {
        var result = await new RunEvaluatedCommand().Exec("systemctl", $"stop {serviceName}");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to stop service {serviceName}: {result.StdOut}");
        }

        return 0;
    }
}