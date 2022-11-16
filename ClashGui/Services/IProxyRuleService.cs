using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Rules;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyRuleService: IObservableListService<RuleInfo, string>, IAutoFreshable
{
    Task UpdateRuleProvider(string name);
}