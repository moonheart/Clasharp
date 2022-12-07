using System.Collections.Generic;
using System.Reactive;
using System.ServiceProcess;
using Clasharp.Interfaces;
using Clasharp.Models.ServiceMode;
using Clasharp.Models.Settings;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignSettingsViewModel:ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";
    public string ClashApiAddress { get; set; }
    public ManagedConfigs ManagedFields { get; set; }
    public SystemProxyMode SystemProxyMode { get; set; }
    public bool UseServiceMode { get; set; }
    public bool IsUninstalled { get; set; }
    public bool IsCoreServiceRunning { get; }
    public ServiceStatus CoreServiceStatus { get; }
    public List<SystemProxyMode> SystemProxyModes { get; }
    public ReactiveCommand<Unit, Unit> InstallService { get; }
    public ReactiveCommand<Unit, Unit> UninstallService { get; }
    public ReactiveCommand<Unit, Unit> StartService { get; }
    public ReactiveCommand<Unit, Unit> StopService { get; }
    public ReactiveCommand<Unit, Unit> ManageCore { get; }
    public Interaction<Unit, Unit> OpenManageCoreWindow { get; }
}