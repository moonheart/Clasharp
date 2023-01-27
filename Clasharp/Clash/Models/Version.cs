using System;

namespace Clasharp.Clash.Models;

public sealed class VersionInfo
{
    public bool Meta { get; set; }

    public string Version { get; set; }

    protected bool Equals(VersionInfo other)
    {
        return Meta == other.Meta && Version == other.Version;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((VersionInfo) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Meta, Version);
    }
}