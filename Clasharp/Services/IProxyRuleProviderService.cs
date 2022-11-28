using System.Collections.Generic;
using Clasharp.Clash.Models.Providers;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public interface IProxyRuleProviderService : IObservableListService<RuleProvider, string>, IAutoFreshable
{
}