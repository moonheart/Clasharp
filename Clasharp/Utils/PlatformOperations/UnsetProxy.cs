using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class UnsetProxy : PlatformSpecificOperation<int>
{
    private readonly RunNormalCommand _runNormalCommand = new();
    protected override Task<int> DoForWindows()
    {
        API_WinProxy.UnsetProxy();
        return Task.FromResult(0);
    }

    protected override async Task<int> DoForLinux()
    {
        await SetForGnome();
        await SetForPlasma();
        return 0;
    }
    
    private async Task SetForGnome()
    {
        var gProxy = "gsettings set org.gnome.system.proxy";
        await _runNormalCommand.Exec($"{gProxy} mode \"none\"");
    }
    private async Task SetForPlasma()
    {
        var kwrite = "kwriteconfig5 --file kioslaverc --group 'Proxy Settings'";
        await _runNormalCommand.Exec($"{kwrite} --key ProxyType 0");
    }
}