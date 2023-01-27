using System;
using System.Net.Http;

namespace Clasharp.Utils;

public static class HttpClientHolder
{
    /// <summary>
    /// HttpClient used for access network resources
    /// </summary>
    public static readonly HttpClient Normal = new()
    {
        DefaultRequestHeaders =
        {
            {"User-Agent", "Clasharp/1.0 (Prefer clash format)"}
        }
    };

    public static HttpClient For(string url, int timeoutSeconds = 10)
    {
        return new HttpClient
        {
            BaseAddress = new Uri(url),
            Timeout = TimeSpan.FromSeconds(timeoutSeconds)
        };
    }
}