using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.ServiceMode;
using ClashGui.Models.Settings;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface ISettingsViewModel: IViewModelBase
{
    ManagedConfigs ManagedFields { get; set; }
    SystemProxyMode SystemProxyMode { get; set; }
    bool UseServiceMode { get; set; }
    bool IsUninstalled { get; }
    bool IsCoreServiceRunning { get; }
    ServiceStatus CoreServiceStatus { get; }
    List<SystemProxyMode> SystemProxyModes { get; }
    ReactiveCommand<Unit, Unit> InstallService { get; }
    ReactiveCommand<Unit, Unit> UninstallService { get; }
    ReactiveCommand<Unit, Unit> StartService { get; }
    ReactiveCommand<Unit, Unit> StopService { get; }
    
}