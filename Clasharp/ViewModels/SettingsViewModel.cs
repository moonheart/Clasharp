using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Themes.Fluent;
using Clasharp.Interfaces;
using Clasharp.Models.ServiceMode;
using Clasharp.Models.Settings;
using Clasharp.Utils;
using Clasharp.Utils.PlatformOperations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Path = System.IO.Path;

namespace Clasharp.ViewModels;

public class SettingsViewModel : ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";
    public ManagedConfigs ManagedFields { get; set; }

    public SettingsViewModel(AppSettings appSettings, CoreServiceHelper coreServiceHelper)
    {
        ManagedFields = appSettings.ManagedFields;
        SystemProxyModes = EnumHelper.GetAllEnumValues<SystemProxyMode>().ToList();
        UseServiceMode = appSettings.UseServiceMode;
        SystemProxyMode = appSettings.SystemProxyMode;
        ThemeMode = appSettings.ThemeMode;

        this.WhenAnyValue(d => d.ThemeMode).Subscribe(d => appSettings.ThemeMode = d);
        this.WhenAnyValue(d => d.SystemProxyMode)
            .Subscribe(d => appSettings.SystemProxyMode = d);
        this.WhenAnyValue(d => d.UseServiceMode)
            .Subscribe(d => appSettings.UseServiceMode = d);
        var serviceStatus = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(async _ => await coreServiceHelper.Status());
        serviceStatus.ToPropertyEx(this, d => d.CoreServiceStatus);
        serviceStatus.Select(d => d == ServiceStatus.Uninstalled).ToPropertyEx(this, d => d.IsUninstalled);
        serviceStatus.Select(d => d == ServiceStatus.Running).ToPropertyEx(this, d => d.IsCoreServiceRunning);

        OpenManageCoreWindow = new Interaction<Unit, Unit>();
        ManageCore = ReactiveCommand.CreateFromTask(async _ => { await OpenManageCoreWindow.Handle(Unit.Default); });
        InstallService = ReactiveCommand.CreateFromTask(async _ =>
        {
            try
            {
                await coreServiceHelper.Install();
            }
            catch (Exception e)
            {
                await ShowError.Handle((e, false));
            }
        });
        UninstallService = ReactiveCommand.CreateFromTask(async _ =>
        {
            try
            {
                await coreServiceHelper.Uninstall();
            }
            catch (Exception e)
            {
                await ShowError.Handle((e, false));
            }
        });
        StartService = ReactiveCommand.CreateFromTask(async _ =>
        {
            try
            {
                await coreServiceHelper.Start();
            }
            catch (Exception e)
            {
                await ShowError.Handle((e, false));
            }
        });
        StopService = ReactiveCommand.CreateFromTask(async _ =>
        {
            try
            {
                await coreServiceHelper.Stop();
            }
            catch (Exception e)
            {
                await ShowError.Handle((e, false));
            }
        });
        SetAutoStart = ReactiveCommand.CreateFromTask(async _ =>
        {
            try
            {
                await new SetAutoStart().Exec(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    Process.GetCurrentProcess().ProcessName));
                await MessageBox.Show("Success", "Success set auto start ");
            }
            catch (Exception e)
            {
                await ShowError.Handle((e, false));
            }
        });
    }

    [Reactive]
    public FluentThemeMode ThemeMode { get; set; }

    [Reactive]
    public SystemProxyMode SystemProxyMode { get; set; }

    [Reactive]
    public bool UseServiceMode { get; set; }

    [ObservableAsProperty]
    public bool IsUninstalled { get; }

    [ObservableAsProperty]
    public bool IsCoreServiceRunning { get; }

    [ObservableAsProperty]
    public ServiceStatus CoreServiceStatus { get; }

    public List<SystemProxyMode> SystemProxyModes { get; }
    public ReactiveCommand<Unit, Unit> InstallService { get; }
    public ReactiveCommand<Unit, Unit> UninstallService { get; }
    public ReactiveCommand<Unit, Unit> StartService { get; }
    public ReactiveCommand<Unit, Unit> StopService { get; }
    public ReactiveCommand<Unit, Unit> SetAutoStart { get; }
    public ReactiveCommand<Unit, Unit> ManageCore { get; }
    public Interaction<Unit, Unit> OpenManageCoreWindow { get; }
}