using System.Text.Json;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class SetProxy: PlatformSpecificOperation<string, int, string[], int>
{
    private readonly RunNormalCommand _runNormalCommand = new();
    protected override Task<int> DoForWindows(string host, int port, string[] exceptions)
    {
        API_WinProxy.SetProxy($"http://{host}:{port}", string.Join(";", exceptions));
        return Task.FromResult(0);
    }

    protected override async Task<int> DoForLinux(string proxy, int port, string[] exceptions)
    {
        await SetForGnome(proxy, port, exceptions);
        await SetForPlasma(proxy, port, exceptions);
        return 0;
    }

    private async Task SetForGnome(string host, int port, string[] exceptions)
    {
        var gProxy = "gsettings set org.gnome.system.proxy";
        await _runNormalCommand.Exec($"{gProxy} mode \"manual\"");
        await _runNormalCommand.Exec($"{gProxy}.http host \"{host}\"");
        await _runNormalCommand.Exec($"{gProxy}.http port {port}");
        await _runNormalCommand.Exec($"{gProxy}.https host \"{host}\"");
        await _runNormalCommand.Exec($"{gProxy}.https port {port}");
        await _runNormalCommand.Exec($"{gProxy}.ftp host \"{host}\"");
        await _runNormalCommand.Exec($"{gProxy}.ftp port {port}");
        await _runNormalCommand.Exec($"{gProxy}.socks host \"{host}\"");
        await _runNormalCommand.Exec($"{gProxy}.socks port {port}");
        await _runNormalCommand.Exec($"{gProxy} ignore-hosts \'{JsonSerializer.Serialize(exceptions)}\'");
    }
    private async Task SetForPlasma(string host, int port, string[] exceptions)
    {
        var kwrite = "kwriteconfig5 --file kioslaverc --group 'Proxy Settings'";
        await _runNormalCommand.Exec($"{kwrite} --key ProxyType 1");
        await _runNormalCommand.Exec($"{kwrite} --key httpProxy \"http://{host}:{port}\"");
        await _runNormalCommand.Exec($"{kwrite} --key httpsProxy \"http://{host}:{port}\"");
        await _runNormalCommand.Exec($"{kwrite} --key ftpProxy \"http://{host}:{port}\"");
        await _runNormalCommand.Exec($"{kwrite} --key socksProxy \"http://{host}:{port}\"");
        await _runNormalCommand.Exec($"{kwrite} --key NoProxyFor \"{string.Join(',', exceptions)}");
    }
}