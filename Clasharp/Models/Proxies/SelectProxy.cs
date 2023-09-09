namespace Clasharp.Models.Proxies;

public class SelectProxy
{
    public string Group { get; set; } = string.Empty;
    public string Proxy { get; set; } = string.Empty;

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