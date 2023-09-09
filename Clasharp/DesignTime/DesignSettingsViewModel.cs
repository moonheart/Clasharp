using System.Collections.Generic;
using System.Reactive;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Clasharp.Interfaces;
using Clasharp.Models.ServiceMode;
using Clasharp.Models.Settings;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignSettingsViewModel:ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";
    public string ClashApiAddress { get; set; } = "";
    public ManagedConfigs ManagedFields { get; set; } = new();
    public ThemeVariant ThemeMode { get; set; } = ThemeVariant.Light;
    public SystemProxyMode SystemProxyMode { get; set; }
    public bool UseSystemCore { get; set; }
    public bool UseServiceMode { get; set; }
    public bool IsUninstalled { get; set; }
    public bool IsCoreServiceRunning { get; } = false;
    public ServiceStatus CoreServiceStatus { get; } = ServiceStatus.Running;
    public List<SystemProxyMode> SystemProxyModes { get; } = new();
    public ReactiveCommand<Unit, Unit> InstallService { get; } = ReactiveCommand.Create(() => { });
    public ReactiveCommand<Unit, Unit> UninstallService { get; } = ReactiveCommand.Create(() => { });
    public ReactiveCommand<Unit, Unit> StartService { get; } = ReactiveCommand.Create(() => { });
    public ReactiveCommand<Unit, Unit> StopService { get; } = ReactiveCommand.Create(() => { });
    public ReactiveCommand<Unit, Unit> SetAutoStart { get; } = ReactiveCommand.Create(() => { });
    public ReactiveCommand<Unit, Unit> ManageCore { get; } = ReactiveCommand.Create(() => {});
    public ReactiveCommand<Unit, Unit> ReloadConfig { get; } = ReactiveCommand.Create(() => { });
    public Interaction<Unit, Unit> OpenManageCoreWindow { get; } = new();
}