namespace ClashGui.Common.ApiModels.Proxies;

public class ProxyGroup
{
    public List<string> All { get; set; } = new();
    public List<ProxyHistory> History { get; set; } = new();

    public string Name { get; set; } = null!;

    public string? Now { get; set; }

    public ProxyGroupType Type { get; set; }

    public bool Udp { get; set; }

    protected bool Equals(ProxyGroup other)
    {
        return All.SequenceEqual(other.All) && History.SequenceEqual(other.History) && Name == other.Name && Now == other.Now && Type == other.Type && Udp == other.Udp;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProxyGroup) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(All, History, Name, Now, (int) Type, Udp);
    }
}