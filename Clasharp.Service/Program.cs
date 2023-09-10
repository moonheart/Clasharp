using System.Net;
using System.Runtime.InteropServices;
using Clasharp.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Clasharp.Service;

static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(options => { options.Listen(IPAddress.Any, GlobalConfigs.ClashServicePort, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; }); });

        builder.Services.AddGrpc();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            builder.Host.UseWindowsService(service => { service.ServiceName = "Clash Gui Service"; });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            builder.Host.UseSystemd();
        }

        var application = builder.Build();

        application.MapGrpcService<CoreServiceImpl>();

        await application.RunAsync();
    }
}