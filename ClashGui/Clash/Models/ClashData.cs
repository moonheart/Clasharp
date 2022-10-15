using System.Collections.Generic;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;

namespace ClashGui.Models;

public interface IClashProxies
{
    Dictionary<string, ProxyGroup>? Proxies { get; set; }
}

public interface IClashRules
{
    List<RuleInfo>? Rules { get; set; }
}

public interface IClashProxyProviders
{
    Dictionary<string, ProxyProvider>? Providers { get; set; }
}

public class ClashData : IClashProxies, IClashRules, IClashProxyProviders
{
    public Dictionary<string, ProxyGroup>? Proxies { get; set; }


    public List<RuleInfo>? Rules { get; set; }
    
    public Dictionary<string, ProxyProvider>? Providers { get; set; }
}