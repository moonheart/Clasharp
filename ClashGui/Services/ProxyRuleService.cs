using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClashGui.Cli;
using ClashGui.Common.ApiModels.Rules;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ProxyRuleService : BaseListService<RuleInfo>, IProxyRuleService
{
    public ProxyRuleService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<List<RuleInfo>> GetObj()
    {
        return (await _clashApiFactory.Get().GetRules())?.Rules ?? new List<RuleInfo>();
    }

    public Task UpdateRuleProvider(string name)
    {
        return _clashApiFactory.Get().UpdateRuleProvider(name);
    }
}