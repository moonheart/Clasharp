using System.Collections.Generic;

namespace ClashGui.Clash.Models.Providers;

public class ProviderData<T>
{
    public Dictionary<string, T>? Providers { get; set; }
}