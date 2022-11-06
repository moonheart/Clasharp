using System.Net;
using ClashGui.WindowsService;

class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var httpListenerWrapper = new HttpListenerWrapper("http://localhost:62134");
        httpListenerWrapper.AddRoute("/start_clash", HandlerStart);
        var cancellationTokenSource = new CancellationTokenSource();
        var t = httpListenerWrapper.Start(cancellationTokenSource.Token);
    }


    private static async Task HandlerStart(HttpListenerContext context)
    {
        
    }
}


