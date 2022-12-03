using System.Threading.Tasks;
using Clasharp.Utils.PlatformOperations;

namespace Clasharp.Utils;

public static class ProxyUtils
{
    public static async Task SetSystemProxy(string strProxy, string exceptions)
    {
        await new SetProxy(strProxy, exceptions).Do();
    }

    public static async Task UnsetSystemProxy()
    {
        await new UnsetProxy().Do();
    }
}