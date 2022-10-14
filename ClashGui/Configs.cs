using System.Net.Http;

namespace ClashGui;

public static class Configs
{
    public static string ControllerApi = "http://127.0.0.1:61708";
    public static string ProxyList = $"{ControllerApi}/proxies";

    public static HttpClient HttpClient = new ();
}