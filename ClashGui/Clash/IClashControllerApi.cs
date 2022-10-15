using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Proxies;
using ClashGui.Models;
using ClashGui.Models.Connections;
using Refit;

namespace ClashGui;

public interface IClashControllerApi
{
    #region Hello

    [Get("/")]
    Task<ClashHello> Hello();

    #endregion

    #region Logs

    /// <summary>
    /// Get real-time logs
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<string> GetRealtimeLogs() => new Streamer("/logs");

    #endregion

    #region Traffic

    /// <summary>
    /// Get real-time traffic data
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<string> GetRealtimeTraffic() => new Streamer("/traffic");

    #endregion

    #region Version

    /// <summary>
    ///  Get clash version
    /// </summary>
    /// <returns></returns>
    [Get("/version")]
    Task<VersionInfo> GetClashVersion();

    #endregion

    #region Configs

    /// <summary>
    /// Get base configs
    /// </summary>
    /// <returns></returns>
    [Get("/configs")]
    Task<Configs> GetBaseConfigs();

    /// <summary>
    /// Update base configs
    /// </summary>
    /// <param name="configs"></param>
    /// <returns></returns>
    [Patch("/configs")]
    Task UpdateBaseConfigs(Configs configs);

    /// <summary>
    /// Reloading base configs
    /// </summary>
    /// <param name="configs"></param>
    /// <returns></returns>
    [Put("/configs")]
    Task ReloadingBaseConfigs(Configs configs);

    #endregion


    #region Proxies

    /// <summary>
    /// Get proxies information
    /// </summary>
    /// <returns></returns>
    [Get("/proxies")]
    Task<ClashData> GetProxyGroups();

    /// <summary>
    /// Get specific proxy information
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Get("/proxies/{name}")]
    Task<ProxyGroup> GetProxyInfo(string name);

    /// <summary>
    /// Select specific proxy
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Put("/proxies/{name}")]
    Task SelectProxy(string name);

    /// <summary>
    /// Get specific proxy delay test information
    /// </summary>
    /// <returns></returns>
    [Get("/proxies/{name}/delay")]
    Task<ProxyDelayInfo> GetProxyDelay(string name, [AliasAs("timeout")] int timeoutMillisecond = 1000);

    #endregion

    #region Rules

    /// <summary>
    /// Get rules information
    /// </summary>
    /// <returns></returns>
    [Get("/rules")]
    Task<ClashData> GetRules();

    #endregion

    #region Connections

    /// <summary>
    /// Get connections information
    /// </summary>
    /// <returns></returns>
    [Get("/connections")]
    Task<ConnectionInfo> GetConnections();

    /// <summary>
    /// Close all connections
    /// </summary>
    /// <returns></returns>
    [Delete("/connections")]
    Task CloseAllConnections();

    /// <summary>
    /// Close specific connection
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Delete("/connections/{id}")]
    Task CloeseConnection(string id);

    #endregion

    #region providers proxies

    /// <summary>
    /// Get all proxies information for all proxy-providers
    /// </summary>
    /// <returns></returns>
    [Get("/providers/proxies")]
    Task<ClashData> GetProxyProviders();

    /// <summary>
    /// Get proxies information for specific proxy-provider
    /// </summary>
    /// <returns></returns>
    [Get("/providers/proxies/{name}")]
    Task<ProxyProvider> GetProxyProvider(string name);

    /// <summary>
    /// Update specific proxy-provider
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Put("/providers/proxies/{name}")]
    Task UpdateProxyProvider(string name);

    /// <summary>
    /// HealthCheck specific proxy-provider
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Get("/providers/proxies/{name}/healthcheck")]
    Task HealthCheckProxyProvider(string name);

    #endregion
}