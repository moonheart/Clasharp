namespace Clasharp.Common;

public static class GlobalConfigs
{
    public static readonly string Userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static readonly string ProgramHome = Path.Combine(Userhome, ".config", "clasharp");
    public static readonly string RuntimeClashConfig = Path.Combine(ProgramHome, "runtime-config.yaml");
    public static readonly string MainConfig = Path.Combine(ProgramHome, "main.json");
    public static readonly string ProfilesDir = Path.Combine(ProgramHome, "profiles/");
    public const string CoreServiceName = "clasharp-core-service"; 
    public const string CoreServiceDesc = "Clasharp Core Service"; 
    public const int ClashServicePort = 61234;
}