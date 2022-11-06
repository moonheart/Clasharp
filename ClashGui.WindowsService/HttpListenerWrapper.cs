using System.Net;

namespace ClashGui.WindowsService;

public class HttpListenerWrapper
{
    private readonly Dictionary<string, Func<HttpListenerContext, CancellationToken, Task>> _routes = new();

    public void AddRoute(string route, Func<HttpListenerContext, CancellationToken, Task> handler) =>
        _routes[route] = handler;

    public async Task Listen(string listenUrl, CancellationToken cancellationToken)
    {
        var httpListener = new HttpListener();
        httpListener.Prefixes.Add(listenUrl);
        httpListener.Start();

        while (!cancellationToken.IsCancellationRequested)
        {
            var httpListenerContext = await httpListener.GetContextAsync();
            var handler = _routes.Keys.FirstOrDefault(d =>
                httpListenerContext.Request.Url?.AbsolutePath.StartsWith(d) ?? false);
            if (handler != null)
            {
                _ = _routes[handler](httpListenerContext, cancellationToken);
            }
            else
            {
                httpListenerContext.Return(404);
            }
        }
    }
}