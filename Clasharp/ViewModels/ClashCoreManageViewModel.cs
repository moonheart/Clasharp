using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Clasharp.Common;
using Clasharp.Interfaces;
using Clasharp.Models.ServiceMode;
using Clasharp.Utils.PlatformOperations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels;

public class ClashCoreManageViewModel : ViewModelBase, IClashCoreManageViewModel
{
    private readonly DownloadCoreServiceBinary _downloadCoreServiceBinary = new();
    private readonly GetServiceStatus _getServiceStatus = new();
    private readonly StartService _startService = new();
    private readonly StopService _stopService = new();
    private readonly GetClashExePath _getClashExePath = new();

    public ClashCoreManageViewModel()
    {
        CustomUrl = "";
        Observable.Timer(TimeSpan.Zero)
            .SelectMany(async _ => await GetClashVersion())
            .ToPropertyEx(this, d => d.CurrentVersion);

        Download = ReactiveCommand.CreateFromTask(async _ =>
        {
            var downloadedPath = await _downloadCoreServiceBinary.Exec(UseCustomUrl ? CustomUrl : null);
            var serviceStatus = await _getServiceStatus.Exec(GlobalConfigs.CoreServiceName);
            var needStartService = false;
            if (serviceStatus == ServiceStatus.Running)
            {
                await _stopService.Exec(GlobalConfigs.CoreServiceName);
                needStartService = true;
            }

            var clashExePath = _getClashExePath.Exec().Result;
            File.Move(downloadedPath, clashExePath, true);
            Observable.Timer(TimeSpan.Zero)
                .SelectMany(async _ => await GetClashVersion())
                .ToPropertyEx(this, d => d.CurrentVersion);
            if (needStartService)
            {
                await _startService.Exec(GlobalConfigs.CoreServiceName);
            }
        });
        Download.IsExecuting.ToPropertyEx(this, d => d.IsDownloading);
    }

    private async Task<string> GetClashVersion()
    {
        var clashExePath = _getClashExePath.Exec().Result;
        var result = await new RunNormalCommand().Exec($"{clashExePath} -v");
        return result.StdOut;
    }

    [ObservableAsProperty]
    public string CurrentVersion { get; }

    [Reactive]
    public bool UseCustomUrl { get; set; }

    [Reactive]
    public string CustomUrl { get; set; }

    public ReactiveCommand<Unit, Unit> Download { get; }

    [ObservableAsProperty]
    public bool IsDownloading { get; }
}