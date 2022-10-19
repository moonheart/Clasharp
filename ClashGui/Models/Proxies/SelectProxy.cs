namespace ClashGui.Models.Proxies;

public class SelectProxy
{
    public string Group { get; set; }
    public string Proxy { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is SelectProxy selectProxy)
        {
            return Proxy == selectProxy.Proxy
                   && Group == selectProxy.Group;
        }

        return false;
    }
}