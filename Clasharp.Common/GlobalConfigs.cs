﻿namespace Clasharp.Common;

public static class GlobalConfigs
{
    public static string Userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static string ProgramHome = Path.Combine(Userhome, ".config", "clasharp");
    public static string RuntimeClashConfig = Path.Combine(ProgramHome, "runtime-config.yaml");
    public static string MainConfig = Path.Combine(ProgramHome, "main.json");
    public static string ProfilesDir = Path.Combine(ProgramHome, "profiles/");
    public static string ClashExe = Path.Combine(ProgramHome, "Clash.Meta-windows-amd64.exe");
    public const string CoreServiceName = "clasharp-core-service"; 
    public const int ClashServicePort = 62134;
}