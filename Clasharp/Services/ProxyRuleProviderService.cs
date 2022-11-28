using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Providers;
using Clasharp.Cli;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public class ProxyRuleProviderService: BaseListService<RuleProvider, string>,IProxyRuleProviderService
{
    public ProxyRuleProviderService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<List<RuleProvider>> GetObj()
    {
        return (await _clashApiFactory.Get().GetRuleProviders())?.Providers?.Values.ToList() ??
               new List<RuleProvider>();
    }

    protected override string GetUniqueKey(RuleProvider obj)
    {
        return obj.Name;
    }
}