using System.IO;
using System.Threading.Tasks;
using Clasharp.Common;

namespace Clasharp.Utils.PlatformOperations;

public class GetClashExePath : PlatformSpecificOperation<bool, string>
{
    protected override Task<string> DoForWindows(bool useSystem)
    {
        return Task.FromResult(useSystem
            ? "clash-meta.exe"
            : Path.Combine(GlobalConfigs.ProgramHome, "clash-meta.exe"));
    }

    protected override Task<string> DoForLinux(bool useSystem)
    {
        return Task.FromResult(useSystem
            ? "clash-meta"
            : Path.Combine(GlobalConfigs.ProgramHome, "clash-meta"));
    }
}