using System.Net;
using System.Text;
using System.Text.Json;

namespace Clasharp.Service;

public static class HttpListenerHelpers
{
    public static async Task<T?> GetRequestBody<T>(this HttpListenerContext context)
    {
        var streamReader = new StreamReader(context.Request.InputStream);
        var reqbody = await streamReader.ReadToEndAsync();
        return JsonSerializer.Deserialize<T>(reqbody, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public static void Return(this HttpListenerContext context, int statusCode = 200)
    {
        context.Response.StatusCode = statusCode;
        context.Response.Close();
    }
    public static void Return(this HttpListenerContext context, string content, int statusCode = 200)
    {
        context.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(content));
        context.Response.OutputStream.FlushAsync();
        context.Return(statusCode);
    }
}