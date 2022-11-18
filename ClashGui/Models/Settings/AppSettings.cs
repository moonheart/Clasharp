using System.Collections.Generic;
using ClashGui.Models.Profiles;

namespace ClashGui.Models.Settings;

public class AppSettings
{
    public SystemProxyMode SystemProxyMode { get; set; }
    public bool UseServiceMode { get; set; }
    public List<Profile> Profiles { get; set; } = new();
    
    public string? SelectedProfile { get; set; }
}