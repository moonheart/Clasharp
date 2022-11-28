using System.Collections.Generic;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Rules;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public interface IProxyRuleService: IObservableListService<RuleInfo, string>, IAutoFreshable
{
    Task UpdateRuleProvider(string name);
}