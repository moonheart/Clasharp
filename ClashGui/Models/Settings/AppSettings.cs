using System.Collections.Generic;
using ClashGui.Models.Profiles;

namespace ClashGui.Models.Settings;

public class AppSettings
{
    public SystemProxyMode SystemProxyMode { get; set; }
    public bool UseServiceMode { get; set; }
    public List<Profile> Profiles { get; set; } = new();
    
    public string? SelectedProfile { get; set; }

    // public Dictionary<ManagedFieldType, ManagedConfig> ManagedFields { get; set; } = new()
    // {
    //     [ManagedFieldType.ExternalController] = new ManagedConfigValue<string>(){Value = "127.0.0.1:19090"}
    // };
    
}

public enum ManagedFieldType
{
    ExternalController
}

public abstract class ManagedConfig
{
    public bool Enabled { get; set; }
}

public static class ManagedConfigExtension
{
    public static T Value<T>(this ManagedConfig managedConfig)
    {
        return ((ManagedConfigValue<T>) managedConfig).Value;
    }
    public static T Set<T>(this ManagedConfig managedConfig, T value)
    {
        return ((ManagedConfigValue<T>) managedConfig).Value = value;
    }
}

public class ManagedConfigValue<T> : ManagedConfig
{
    public T Value { get; set; }
}