using System;
using System.Collections.Generic;
using ClashGui.Clash.Models.Proxies;

namespace ClashGui.Clash.Models.Providers;

public class ProxyProvider
{
    public string Name { get; set; }
    public List<ProxyGroup> Proxies { get; set; }
    public string Type { get; set; }
    public DateTime UpdatedAt { get; set; }
    public VehicleType VehicleType { get; set; }
}