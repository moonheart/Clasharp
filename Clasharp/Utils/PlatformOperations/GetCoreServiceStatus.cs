using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.ServiceProcess;
using System.Threading.Tasks;
using Clasharp.Models.ServiceMode;

namespace Clasharp.Utils.PlatformOperations;

public class GetServiceStatus : PlatformSpecificOperation<string, ServiceStatus>
{
    /// <summary>
    /// GetServiceStatus
    /// </summary>
    /// <param name="serviceName">serviceName</param>
    /// <returns></returns>
    public override Task<ServiceStatus> Exec(string serviceName)
    {
        return base.Exec(serviceName);
    }

    [SupportedOSPlatform("Windows")]
    protected override Task<ServiceStatus> DoForWindows(string serviceName)
    {
        try
        {
            var serviceController = new ServiceController(serviceName);
            return Task.FromResult((ServiceStatus) (int) serviceController.Status);
        }
        catch
        {
            return Task.FromResult(ServiceStatus.Uninstalled);
        }
    }

    protected override async Task<ServiceStatus> DoForLinux(string serviceName)
    {
        var process = Process.Start(new ProcessStartInfo("systemctl",
            $"show {serviceName} --no-pager -p ActiveState,LoadState,SubState")
        {
            RedirectStandardOutput = true
        });
        if (process == null) throw new InvalidOperationException("Failed to get service status");

        await process.WaitForExitAsync();
        var output = await process.StandardOutput.ReadToEndAsync();
        /*
         * LoadState=loaded
           ActiveState=active
           SubState=running
         */
        Dictionary<string, string> show = new();
        foreach (var s in output.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            var strings = s.Split('=');
            if (strings.Length != 2) continue;
            show.Add(strings[0], strings[1]);
        }

        if (show["LoadState"] != "loaded")
        {
            return ServiceStatus.Uninstalled;
        }

        // https://man7.org/linux/man-pages/man5/org.freedesktop.systemd1.5.html
        return show["ActiveState"] switch
        {
            "active" => ServiceStatus.Running,
            "reloading" => ServiceStatus.StartPending,
            "inactive" => ServiceStatus.Stopped,
            "failed" => ServiceStatus.Stopped,
            "activating" => ServiceStatus.StartPending,
            "deactivating" => ServiceStatus.StopPending,
            _ => throw new Exception($"unknown ActiveState {show["ActiveState"]}")
        };
    }
}