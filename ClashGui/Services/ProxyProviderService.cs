using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Providers;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ProxyProviderService : BaseListService<ProxyProvider>, IProxyProviderService
{
    public ProxyProviderService(IClashCli clashCli) : base(clashCli)
    {
    }

    protected override async Task<List<ProxyProvider>> GetObj()
    {
        var providerData = await GlobalConfigs.ClashControllerApi.GetProxyProviders();
        return providerData?.Providers?.Values.Where(d =>
                   d.VehicleType != VehicleType.Compatible && d.VehicleType != VehicleType.Unknown).ToList() ??
               new List<ProxyProvider>();
    }
}