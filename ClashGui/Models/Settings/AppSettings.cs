using System.Collections.Generic;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Models.Settings;

public class AppSettings: ReactiveObject
{
    public SystemProxyMode SystemProxyMode { get; set; }
    public bool UseServiceMode { get; set; }
    public List<Profile> Profiles { get; set; } = new();
    
    public string? SelectedProfile { get; set; }

    public ManagedConfigs ManagedFields { get; set; } = new();

}

public class ManagedConfigs: ReactiveObject
{
    public ManagedConfigValue<int> ExternalControllerPort { get; set; } = new() {Value = 19090, Hide = false};
}

public abstract class ManagedConfig
{
    public bool Enabled { get; set; }
    public bool Hide { get; set; }
}

public class ManagedConfigValue<T> : ManagedConfig
{
    public T? Value { get; set; }
}