namespace ClashGui.Services;

public class ClashService: IClashService
{
    public ClashService(IProxyGroupService proxyGroupService)
    {
        ProxyGroupService = proxyGroupService;
    }
    
    public IProxyGroupService ProxyGroupService { get; }
}