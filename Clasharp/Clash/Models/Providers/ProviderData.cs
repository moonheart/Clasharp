using System.Collections.Generic;

namespace Clasharp.Clash.Models.Providers;

public class ProviderData<T>
{
    public Dictionary<string, T>? Providers { get; set; }
}