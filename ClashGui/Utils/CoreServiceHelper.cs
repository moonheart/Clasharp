using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using ClashGui.Models.ServiceMode;

namespace ClashGui.Utils;

public class CoreServiceHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="Exception">when error happens</exception>
    public async Task Install()
    {
        // var path = Path.Combine(Environment.CurrentDirectory, "ClashGui.WindowsService");
        var path =
            @"D:\Git_Github\ClashGui\ClashGui\ClashGui.WindowsService\bin\Debug\net6.0\ClashGui.WindowsService.exe";
        if (!await sc($"create clash_gui_service binPath=\"{path}\""))
        {
            throw new Exception("Install core service failed");
        }
        //
        // if (!await sc($"start clash_gui_service"))
        // {
        //     throw new Exception("Start core service failed");
        // }
    }

    public async Task Uninstall()
    {
        if (!await sc($"delete clash_gui_service"))
        {
            throw new Exception("Delete core service failed");
        }
        if (!await sc($"stop clash_gui_service"))
        {
            throw new Exception("Stop core service failed");
        }
    }

    public async Task<ServiceStatus> Status()
    {
        try
        {
            var serviceController = new ServiceController("clash_gui_service");
            return (ServiceStatus) (int) serviceController.Status;
        }
        catch (Exception e)
        {
            return ServiceStatus.Uninstalled;
        }
    }

    public async Task Start()
    {
        var serviceController = new ServiceController("clash_gui_service");
        serviceController.Start();
    }

    public async Task Stop()
    {
        var serviceController = new ServiceController("clash_gui_service");
        serviceController.Stop();
    }

    private async Task<bool> sc(string args)
    {
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "sc",
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = true,
                Verb = "runas"
            },
        };
        process.Start();
        await process.WaitForExitAsync();
        return process.ExitCode == 0;
    }
}