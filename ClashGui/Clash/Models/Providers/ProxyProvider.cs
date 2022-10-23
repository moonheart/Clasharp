using System;
using System.Collections.Generic;
using System.Linq;
using ClashGui.Clash.Models.Proxies;

namespace ClashGui.Clash.Models.Providers;

public class ProxyProvider
{
    public string Name { get; set; }
    public List<ProxyGroup> Proxies { get; set; }
    public string Type { get; set; }
    public DateTime UpdatedAt { get; set; }
    public VehicleType VehicleType { get; set; }

    protected bool Equals(ProxyProvider other)
    {
        return Name == other.Name && Proxies.SequenceEqual(other.Proxies) && Type == other.Type && UpdatedAt.Equals(other.UpdatedAt) && VehicleType == other.VehicleType;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProxyProvider) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Proxies, Type, UpdatedAt, (int) VehicleType);
    }
}