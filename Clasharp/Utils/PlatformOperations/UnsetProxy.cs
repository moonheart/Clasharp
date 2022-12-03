using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class UnsetProxy : PlatformSpecificOperation<int>
{
    protected override Task<int> DoForWindows()
    {
        API_WinProxy.UnsetProxy();
        return Task.FromResult(0);
    }
}