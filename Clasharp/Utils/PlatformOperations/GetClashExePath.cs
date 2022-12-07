using System.IO;
using System.Threading.Tasks;
using Clasharp.Common;

namespace Clasharp.Utils.PlatformOperations;

public class GetClashExePath: PlatformSpecificOperation<string>
{
    protected override Task<string> DoForWindows()
    {
        return Task.FromResult(Path.Combine(GlobalConfigs.ProgramHome, "clash-meta.exe"));
    }

    protected override Task<string> DoForLinux()
    {
        return Task.FromResult(Path.Combine(GlobalConfigs.ProgramHome, "clash-meta"));
    }
}