using System;

namespace ClashGui.Models.Profiles;

public class Profile
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }

    public string? Filename { get; set; }

    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public ProfileType Type { get; set; }
    
    public string? FromFile { get; set; }
    
    public string? RemoteUrl { get; set; }
    public int? UpdateInterval { get; set; }

}
