namespace Clasharp.Common;

public static class GlobalConfigs
{
    public static string Userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static string ProgramHome = Path.Combine(Userhome, ".config", "clasharp");
    public static string RuntimeClashConfig = Path.Combine(ProgramHome, "runtime-config.yaml");
    public static string MainConfig = Path.Combine(ProgramHome, "main.json");
    public static string ProfilesDir = Path.Combine(ProgramHome, "profiles/");
    public const string CoreServiceName = "clasharp-core-service"; 
    public const string CoreServiceDesc = "Clasharp Core Service"; 
    public const int ClashServicePort = 61234;
}