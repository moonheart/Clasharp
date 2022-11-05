using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Providers;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ProxyRuleProviderService: BaseService<List<RuleProvider>>,IProxyRuleProviderService
{
    public ProxyRuleProviderService(IClashCli clashCli) : base(clashCli)
    {
    }

    protected override async Task<List<RuleProvider>> GetObj()
    {
        return (await GlobalConfigs.ClashControllerApi.GetRuleProviders())?.Providers?.Values.ToList() ??
               new List<RuleProvider>();
    }

    protected override bool ObjEquals(List<RuleProvider> oldObj, List<RuleProvider> newObj)
    {
        return oldObj.SequenceEqual(newObj);
    }
}