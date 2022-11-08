namespace ClashGui.WindowsService;

class Program
{
    public static async Task Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .UseWindowsService(service =>
            {
                service.ServiceName = "Clash Gui Service";
            })
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<HttpListenerWrapper>();
            })
            .Build();

        await host.RunAsync();
    }

}