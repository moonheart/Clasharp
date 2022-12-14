using System;
using System.IO;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class InstallService : PlatformSpecificOperation<string, string, string, int>
{
    private readonly RunEvaluatedCommand _evaluatedCommand = new();

    /// <summary>
    /// Install service
    /// </summary>
    /// <param name="serviceName">service name</param>
    /// <param name="desc">service description</param>
    /// <param name="exePath">service binary path</param>
    /// <returns></returns>
    public override Task<int> Exec(string serviceName, string desc, string exePath)
    {
        return base.Exec(serviceName, desc, exePath);
    }

    protected override async Task<int> DoForWindows(string serviceName, string desc, string exePath)
    {
        var result = await _evaluatedCommand.Exec("sc",
            $"create {serviceName} binPath=\"{exePath}\" start=auto DisplayName=\"{desc}\"");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to install service {serviceName}: {result.StdOut}");
        }

        return 0;
    }

    protected override async Task<int> DoForLinux(string serviceName, string desc, string exePath)
    {
        var workDir = Path.GetDirectoryName(exePath);
        var unitFileContent = @$"
[Unit]
Description={desc}
After=network.target
Wants=network-online.target
[Service]
Restart=always
Type=simple
ExecStart={exePath}
WorkingDirectory={workDir}
[Install]
WantedBy=multi-user.target";
        var tempFile = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFile, unitFileContent);
        var result = await _evaluatedCommand.Exec("sh",
            $"-c 'install -m 644 {tempFile} /etc/systemd/system/{serviceName}.service && systemctl daemon-reload && systemctl enable {serviceName}.service'");
        if (result.ExitCode != 0)
        {
            throw new Exception($"Failed to install service {serviceName}: {result.StdOut}");
        }
        return 0;
    }
}