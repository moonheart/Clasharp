using System.Net.Http;
using ClashGui.Clash;
using Refit;

namespace ClashGui;

public static class GlobalConfigs
{
    public static string ControllerApi = "http://127.0.0.1:61708";

    public static IClashControllerApi ClashControllerApi = RestService.For<IClashControllerApi>(ControllerApi);

}