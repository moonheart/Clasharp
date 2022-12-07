using System.Net.Http;
using System.Net.Http.Headers;

namespace Clasharp.Utils;

public static class HttpClientHolder
{
    /// <summary>
    /// HttpClient used for access network resources
    /// </summary>
    public static HttpClient Normal = new()
    {
        DefaultRequestHeaders =
        {
            {"User-Agent", "Clasharp/1.0 (Prefer clash format)"}
        }
    };
}