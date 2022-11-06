using System.Net;

namespace ClashGui.WindowsService;

public class HttpListenerWrapper
{
    private HttpListener _httpListener;
    private string _listenUrl;
    private Dictionary<string, Func<HttpListenerContext, Task>> _routes = new();

    public HttpListenerWrapper(string listenUrl)
    {
        _listenUrl = listenUrl;
    }

    public void AddRoute(string route, Func<HttpListenerContext, Task> handler) => _routes[route] = handler;

    public async Task Start( CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(_listenUrl);
            _httpListener.Start();

            var httpListenerContext = await _httpListener.GetContextAsync();
            var handler = _routes.Keys.FirstOrDefault(d =>
                httpListenerContext.Request.Url?.AbsolutePath.StartsWith(d) ?? false);
            if (handler != null)
            {
                _ = _routes[handler](httpListenerContext);
            }
            else
            {
                httpListenerContext.Response.StatusCode = 404;
                httpListenerContext.Response.Close();
            }
        }
    }
}