using System.Collections.Generic;
using ClashGui.Clash.Models.Providers;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyRuleProviderService: IObservalbeObjService<List<RuleProvider>>, IAutoFreshable
{
    
}