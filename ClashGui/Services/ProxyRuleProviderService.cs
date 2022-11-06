using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClashGui.Cli;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ProxyRuleProviderService: BaseListService<RuleProvider>,IProxyRuleProviderService
{
    public ProxyRuleProviderService(IClashCli clashCli) : base(clashCli)
    {
    }

    protected override async Task<List<RuleProvider>> GetObj()
    {
        return (await GlobalConfigs.ClashControllerApi.GetRuleProviders())?.Providers?.Values.ToList() ??
               new List<RuleProvider>();
    }

}