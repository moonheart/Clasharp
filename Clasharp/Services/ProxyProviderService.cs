using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Providers;
using Clasharp.Cli;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public class ProxyProviderService : BaseListService<ProxyProvider, string>, IProxyProviderService
{
    public ProxyProviderService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<List<ProxyProvider>> GetObj()
    {
        var providerData = await _clashApiFactory.Get().GetProxyProviders();
        return providerData?.Providers?.Values.Where(d =>
                   d.VehicleType != VehicleType.Compatible && d.VehicleType != VehicleType.Unknown).ToList() ??
               new List<ProxyProvider>();
    }

    public Task HealthCheckProxyProvider(string name)
    {
        return _clashApiFactory.Get().HealthCheckProxyProvider(name);
    }

    public Task UpdateProxyProvider(string name)
    {
        return _clashApiFactory.Get().UpdateProxyProvider(name);
    }

    protected override string GetUniqueKey(ProxyProvider obj)
    {
        return obj.Name;
    }
}