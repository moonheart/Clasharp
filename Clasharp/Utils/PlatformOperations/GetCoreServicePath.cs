using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class GetCoreServicePath: PlatformSpecificOperation<string>
{
    protected override Task<string> DoForWindows()
    {
#if DEBUG
        return Task.FromResult(@"D:\Git_Github\Clasharp\Clasharp.Service\bin\Debug\net6.0\Clasharp.Service.exe");
#else
        return Task.FromResult(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Clasharp.Service.exe"));
#endif
    }

    protected override Task<string> DoForLinux()
    {
#if DEBUG
        return Task.FromResult(System.IO.Path.Combine(System.Environment.CurrentDirectory, "../../../../../Clasharp.Service/bin/Debug/net6.0/Clasharp.Service"));
#else
        return Task.FromResult(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Clasharp.WindowsService"));
#endif

    }
}