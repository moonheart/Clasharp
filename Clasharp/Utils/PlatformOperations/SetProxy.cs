using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class SetProxy: PlatformSpecificOperation<int>
{
    private readonly string _proxy;
    private readonly string _exceptions;
    
    public SetProxy(string proxy, string exceptions)
    {
        _proxy = proxy;
        _exceptions = exceptions;
    }

    protected override Task<int> DoForWindows()
    {
        API_WinProxy.SetProxy(_proxy, _exceptions);
        return Task.FromResult(0);
    }
    
}