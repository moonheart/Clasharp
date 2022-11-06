using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Providers;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ProxyRuleProviderService: BaseListService<RuleProvider>,IProxyRuleProviderService
{
    public ProxyRuleProviderService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<List<RuleProvider>> GetObj()
    {
        return (await _clashApiFactory.Get().GetRuleProviders())?.Providers?.Values.ToList() ??
               new List<RuleProvider>();
    }

}