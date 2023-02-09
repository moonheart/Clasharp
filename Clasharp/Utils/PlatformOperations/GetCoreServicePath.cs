using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class GetCoreServicePath: PlatformSpecificOperation<string>
{
    protected override Task<string> DoForWindows()
    {
        return Task.FromResult(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Clasharp.Service.exe"));
    }

    protected override Task<string> DoForLinux()
    {
        return Task.FromResult(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Clasharp.Service"));
    }
}