using System;

namespace ClashGui.Clash.Models.Proxies;

public class ProxyHistory
{
    public DateTime Time { get; set; }

    public int Delay { get; set; }

    protected bool Equals(ProxyHistory other)
    {
        return Time.Equals(other.Time) && Delay == other.Delay;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProxyHistory) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Time, Delay);
    }
}