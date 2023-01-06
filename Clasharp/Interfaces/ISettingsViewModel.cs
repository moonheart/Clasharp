using System.Collections.Generic;
using System.Reactive;
using Avalonia.Themes.Fluent;
using Clasharp.Models.ServiceMode;
using Clasharp.Models.Settings;
using ReactiveUI;

namespace Clasharp.Interfaces;

public interface ISettingsViewModel: IViewModelBase
{
    ManagedConfigs ManagedFields { get; set; }
    FluentThemeMode ThemeMode { get; set; }
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
    ReactiveCommand<Unit, Unit> SetAutoStart { get; }
    ReactiveCommand<Unit, Unit> ManageCore { get; }
    ReactiveCommand<Unit, Unit> ReloadConfig { get; }
    Interaction<Unit, Unit> OpenManageCoreWindow { get; }
}