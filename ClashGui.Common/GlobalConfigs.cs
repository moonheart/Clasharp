namespace ClashGui.Common;

public static class GlobalConfigs
{
    public static string Userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static string ProgramHome = Path.Combine(Userhome, ".config", "clashgui");
    public static string ClashConfig = Path.Combine(ProgramHome, "config.yaml");
    public static string MainConfig = Path.Combine(ProgramHome, "main.json");
    public static string ProfilesDir = Path.Combine(ProgramHome, "profiles/");
    public static string ClashExe = Path.Combine(ProgramHome, "Clash.Meta-windows-amd64.exe");
    public const int ClashServicePort = 62134;
}