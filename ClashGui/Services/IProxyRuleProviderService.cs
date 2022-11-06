using System.Collections.Generic;
using ClashGui.Common.ApiModels.Providers;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProxyRuleProviderService: IObservalbeObjService<List<RuleProvider>>, IAutoFreshable
{
    
}