namespace ClashGui.Common.ApiModels.Proxies;

public enum ProxyGroupType
{
    Direct,
    Reject,

    Shadowsocks,
    ShadowsocksR,
    Snell,
    Socks5,
    Http,
    Vmess,
    Trojan,

    Relay,
    Selector,
    Fallback,
    URLTest,
    LoadBalance,

    Compatible,
    Pass,
}