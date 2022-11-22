using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Interfaces;
using ClashGui.Models.ServiceMode;
using ClashGui.Models.Settings;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class SettingsViewModel : ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";
    public AppSettings AppSettings { get; set; }

    public SettingsViewModel(AppSettings appSettings, CoreServiceHelper coreServiceHelper)
    {
        AppSettings = appSettings;
        SystemProxyModes = EnumHelper.GetAllEnumValues<SystemProxyMode>().ToList();
        UseServiceMode = AppSettings.UseServiceMode;
        SystemProxyMode = AppSettings.SystemProxyMode;

        this.WhenAnyValue(d => d.SystemProxyMode)
            .Subscribe(d => AppSettings.SystemProxyMode = d);
        this.WhenAnyValue(d => d.UseServiceMode)
            .Subscribe(d => AppSettings.UseServiceMode = d);
        var serviceStatus = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .SelectMany(async _ => await coreServiceHelper.Status());
        serviceStatus.ToPropertyEx(this, d => d.CoreServiceStatus);
        serviceStatus.Select(d => d == ServiceStatus.Uninstalled).ToPropertyEx(this, d => d.IsUninstalled);
        serviceStatus.Select(d => d == ServiceStatus.Running).ToPropertyEx(this, d => d.IsCoreServiceRunning);

        InstallService = ReactiveCommand.CreateFromTask(async _ =>
        {
            try
            {
                await coreServiceHelper.Install();
            }
            catch (Exception e)
            {
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", e.Message);
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
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", e.Message);
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
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", e.Message);
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
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", e.Message);
            }
        });
    }

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

    public int ExternalController //{ get; set; }
    {
        get => AppSettings.ManagedFields.ExternalControllerPort.Value;
        set
        {
            this.RaisePropertyChanging();
            if (AppSettings.ManagedFields.ExternalControllerPort.Value == value) return;
            AppSettings.ManagedFields.ExternalControllerPort.Value = value;
            this.RaisePropertyChanged();
        }
    }
}