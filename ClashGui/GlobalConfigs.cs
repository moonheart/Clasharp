using System;
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

}