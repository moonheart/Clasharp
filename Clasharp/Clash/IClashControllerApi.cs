using System.Collections.Generic;
using System.Threading.Tasks;
using Clasharp.Clash.Models;
using Clasharp.Clash.Models.Connections;
using Clasharp.Clash.Models.Providers;
using Clasharp.Clash.Models.Proxies;
using Clasharp.Clash.Models.Rules;
using Refit;

namespace Clasharp.Clash;

public interface IClashControllerApi
{
    #region Hello

    [Get("/")]
    Task<ClashHello?> Hello();

    #endregion

    #region Logs

    /// <summary>
    /// Get real-time logs
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<string> GetRealtimeLogs(string uri) => new Streamer(uri, "/logs");

    #endregion

    #region Traffic

    /// <summary>
    /// Get real-time traffic data
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<string> GetRealtimeTraffic(string uri) => new Streamer(uri, "/traffic");

    #endregion

    #region Version

    /// <summary>
    ///  Get clash version
    /// </summary>
    /// <returns></returns>
    [Get("/version")]
    Task<VersionInfo?> GetClashVersion();

    #endregion

    #region Configs

    /// <summary>
    /// Get base configs
    /// </summary>
    /// <returns></returns>
    [Get("/configs")]
    Task<Configs?> GetConfigs();

    /// <summary>
    /// Reloading base configs
    /// </summary>
    /// <param name="updateConfigRequest"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    [Put("/configs?force={force}")]
    Task UpdateConfigs(UpdateConfigRequest updateConfigRequest, bool force = false);

    /// <summary>
    /// Update base configs
    /// </summary>
    /// <param name="configs"></param>
    /// <returns></returns>
    [Patch("/configs")]
    Task UpdateBaseConfigs(Configs configs);

    #endregion


    #region Proxies

    /// <summary>
    /// Get proxies information
    /// </summary>
    /// <returns></returns>
    [Get("/proxies")]
    Task<ProxyData?> GetProxyGroups();

    /// <summary>
    /// Get specific proxy information
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Get("/proxies/{name}")]
    Task<ProxyGroup?> GetProxyInfo(string name);

    /// <summary>
    /// Select specific proxy
    /// </summary>
    /// <param name="name"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [Put("/proxies/{name}")]
    Task SelectProxy(string name, [Body] UpdateProxyRequest request);

    /// <summary>
    /// Get specific proxy delay test information
    /// </summary>
    /// <returns></returns>
    [Get("/proxies/{name}/delay")]
    Task<ProxyDelayInfo?> GetProxyDelay(string name, [AliasAs("timeout")] int timeoutMillisecond = 1000);

    #endregion

    #region Rules

    /// <summary>
    /// Get rules information
    /// </summary>
    /// <returns></returns>
    [Get("/rules")]
    Task<RuleData?> GetRules();

    #endregion

    #region Connections

    /// <summary>
    /// Get connections information
    /// </summary>
    /// <returns></returns>
    [Get("/connections")]
    Task<ConnectionInfo?> GetConnections();

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
    Task CloseConnection(string id);

    #endregion

    #region providers proxies

    /// <summary>
    /// Get all proxies information for all proxy-providers
    /// </summary>
    /// <returns></returns>
    [Get("/providers/proxies")]
    Task<ProviderData<ProxyProvider>?> GetProxyProviders();

    /// <summary>
    /// Get proxies information for specific proxy-provider
    /// </summary>
    /// <returns></returns>
    [Get("/providers/proxies/{name}")]
    Task<ProxyProvider?> GetProxyProvider(string name);

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

    #region rules proxies

    /// <summary>
    /// Get all rules information for all rule-providers
    /// </summary>
    /// <returns></returns>
    [Get("/providers/rules")]
    Task<ProviderData<RuleProvider>?> GetRuleProviders();

    /// <summary>
    /// Get rules information for specific rule-provider
    /// </summary>
    /// <returns></returns>
    [Get("/providers/rules/{name}")]
    Task<ProxyProvider?> GetRuleProvider(string name);

    /// <summary>
    /// Update specific rule-provider
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Put("/providers/rules/{name}")]
    Task UpdateRuleProvider(string name);

    /// <summary>
    /// HealthCheck specific rule-provider
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Get("/providers/rules/{name}/healthcheck")]
    Task HealthCheckRuleProvider(string name);

    #endregion
}