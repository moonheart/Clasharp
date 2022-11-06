using System.Collections.Generic;
using ClashGui.Common.ApiModels.Rules;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyRuleService: IObservalbeObjService<List<RuleInfo>>, IAutoFreshable
{
    
}