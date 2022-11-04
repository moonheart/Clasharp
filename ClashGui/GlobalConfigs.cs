using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ClashGui.Clash;
using Refit;

namespace ClashGui;

public static class GlobalConfigs
{
    public static string ControllerApi = "http://127.0.0.1:51708";

    public static IClashControllerApi ClashControllerApi = RestService.For<IClashControllerApi>(ControllerApi, new RefitSettings()
    {
        ExceptionFactory = message => Task.FromResult<Exception?>(null)
    });
    
    public static string Userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    public static string ProgramHome = Path.Combine(Userhome, ".config", "clashgui");
    public static string ClashConfig = Path.Combine(ProgramHome, "config.yaml");
    public static string MainConfig = Path.Combine(ProgramHome, "main.json");
    public static string ClashExe = Path.Combine(ProgramHome, "Clash.Meta-windows-amd64.exe");
    // private static string _clashExe = Path.Combine(_programHome, "clash-windows-amd64.exe");


}