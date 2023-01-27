using System;

namespace Clasharp.Clash.Models.Proxies;

public sealed class ProxyHistory
{
    public DateTime Time { get; set; }

    public int Delay { get; set; }

    protected bool Equals(ProxyHistory other)
    {
        return Time.ToString("s").Equals(other.Time.ToString("s")) && Delay == other.Delay;
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