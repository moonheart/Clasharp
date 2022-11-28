using System;

namespace Clasharp.Clash.Models.Rules;

public class RuleInfo
{
    public string Type { get; set; }


    public string Payload { get; set; }


    public string Proxy { get; set; }

    protected bool Equals(RuleInfo other)
    {
        return Type == other.Type && Payload == other.Payload && Proxy == other.Proxy;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RuleInfo) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Payload, Proxy);
    }
}