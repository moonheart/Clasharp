using System;

namespace Clasharp.Clash.Models.Providers;

public sealed class RuleProvider
{
    public string Behavior { get; set; } = "";
    public string Name { get; set; } = "";
    public int RuleCount { get; set; }
    public string Type { get; set; } = "";
    public DateTime UpdatedAt { get; set; }
    public VehicleType VehicleType { get; set; }

    protected bool Equals(RuleProvider other)
    {
        return Behavior == other.Behavior && Name == other.Name && RuleCount == other.RuleCount && Type == other.Type && UpdatedAt.ToString("s").Equals(other.UpdatedAt.ToString("s")) && VehicleType == other.VehicleType;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RuleProvider) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Behavior, Name, RuleCount, Type, UpdatedAt, (int) VehicleType);
    }
}