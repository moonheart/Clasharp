using System.Net.Http;

namespace Clasharp.Utils;

public static class HttpClientHolder
{
    /// <summary>
    /// HttpClient used for access network resources
    /// </summary>
    public static HttpClient Normal = new();
}