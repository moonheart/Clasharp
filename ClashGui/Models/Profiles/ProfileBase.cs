using System;

namespace ClashGui.Models.Profiles;

public abstract class ProfileBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Notes { get; set; }
    
    public string Filename { get; set; }

    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public abstract ProfileType Type { get; }
}

public class LocalProfile : ProfileBase
{
    public override ProfileType Type => ProfileType.Local;
    
    public string FromFile { get; set; }
}

public class RemoteProfile : ProfileBase
{
    public string RemoteUrl { get; set; }

    public TimeSpan UpdateInterval { get; set; }
    public override ProfileType Type => ProfileType.Remote;
}