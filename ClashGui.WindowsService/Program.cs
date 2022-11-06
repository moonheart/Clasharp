namespace ClashGui.WindowsService;

class Program
{
    public static async Task Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<HttpListenerWrapper>();
            })
            .Build();

        await host.RunAsync();
    }

}