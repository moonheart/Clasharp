#if Linux

using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using Clasharp.Models.ServiceMode;
using Serilog;

namespace Clasharp.Utils;

public class CoreServiceHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="Exception">when error happens</exception>
    public async Task Install()
    {
#if DEBUG
        var path = System.IO.Path.Combine(Environment.CurrentDirectory, "../../../../../Clasharp.Service/bin/Debug/net6.0/Clasharp.Service");
#else
        var path = System.IO.Path.Combine(Environment.CurrentDirectory, "Clasharp.WindowsService");
#endif
        var unitFileContent = @$"
[Unit]
Description=Clasharp core service
After=network.target
Wants=network-online.target
[Service]
Restart=always
Type=simple
ExecStart={path}
[Install]
WantedBy=multi-user.target";
        var tempFile = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFile, unitFileContent);
        
        if (!await sc("cp",$"{tempFile} /etc/systemd/system/clasharp-core.service"))
        {
            throw new Exception("Install core service failed");
        }
        
        if (!await sc("systemctl", $"enable --now clasharp-core.service"))
        {
            throw new Exception("Start core service failed");
        }
    }

    public async Task Uninstall()
    {
        if (!await sc("systemctl", $"disable --now clasharp-core.service"))
        {
            throw new Exception("Stop core service failed");
        }
        if (!await sc("rm",$"/etc/systemd/system/clasharp-core.service"))
        {
            throw new Exception("Delete core service failed");
        }
    }

    public async Task<ServiceStatus> Status()
    {
        Log.Information("start check linux status");
        try
        {
            sc("systemctl", $"systemctl status --no-pager -n 0 clasharp-core.service");
            var active = await sc_out("systemctl", $"is-active clasharp-core.service");
            Log.Information($"end check status {active}");
            if (active == "active")
            {
                return ServiceStatus.Running;
            }
            else
            {
                return ServiceStatus.Stopped;
            }
        }
        catch (Exception e)
        {
            Log.Error(e, "status failed");
            return ServiceStatus.Uninstalled;
        }
   
    }

    public async Task Start()
    {
        if (!await sc("systemctl", $"start clasharp-core.service"))
        {
            throw new Exception("Start core service failed");
        }
    }

    public async Task Stop()
    {
        if (!await sc("systemctl", $"stop clasharp-core.service"))
        {
            throw new Exception("Stop core service failed");
        }
    }

    private async Task<bool> sc(string name, string args)
    {
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = name,
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = true,
                UserName = "root",
                Verb = "runas"
            },
        };
        process.Start();
        await process.WaitForExitAsync();
        return process.ExitCode == 0;
    }
    private async Task<string> sc_out(string name, string args)
    {
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = name,
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = false,
                // UserName = "root",
                // Verb = "runas",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            },
        };
        process.Start();
        await process.WaitForExitAsync();
        return await process.StandardOutput.ReadToEndAsync();
    }
}

#endif