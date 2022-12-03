using System.Threading.Tasks;
using Clasharp.Utils.PlatformOperations;

namespace Clasharp.Utils;

public static class ProxyUtils
{
    private static readonly SetProxy SetProxy = new();
    private static readonly UnsetProxy UnsetProxy = new();
    public static async Task SetSystemProxy(string strProxy, string exceptions)
    {
        await SetProxy.Exec(strProxy, exceptions);
    }

    public static async Task UnsetSystemProxy()
    {
        await UnsetProxy.Exec();
    }
}