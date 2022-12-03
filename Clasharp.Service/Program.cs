using System.Runtime.InteropServices;

namespace Clasharp.Service;

class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<HttpListenerWrapper>();
            });

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            hostBuilder.UseWindowsService(service =>
            {
                service.ServiceName = "Clash Gui Service";
            });
        }else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            hostBuilder.UseSystemd();
        }

        var host = hostBuilder.Build();
        await host.RunAsync();
    }

}