using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class SetProxy: PlatformSpecificOperation<string, string, int>
{
    protected override Task<int> DoForWindows(string proxy, string exceptions)
    {
        API_WinProxy.SetProxy(proxy, exceptions);
        return Task.FromResult(0);
    }
    
}