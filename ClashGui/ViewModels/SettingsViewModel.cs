using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.Models.Settings;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class SettingsViewModel : ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";
    private AppSettings _appSettings;

    public SettingsViewModel(AppSettings appSettings, CoreServiceHelper coreServiceHelper)
    {
        _appSettings = appSettings;
        SystemProxyModes = EnumHelper.GetAllEnumValues<SystemProxyMode>().ToList();
        UseServiceMode = _appSettings.UseServiceMode;
        SystemProxyMode = _appSettings.SystemProxyMode;

        this.WhenAnyValue(d => d.SystemProxyMode)
            .Subscribe(d => _appSettings.SystemProxyMode = d);
        this.WhenAnyValue(d => d.UseServiceMode)
            .Subscribe(d => _appSettings.UseServiceMode = d);

        SetServiceModeCommand = ReactiveCommand.CreateFromTask<bool>(async b =>
        {
            try
            {
                if (b)
                {
                    await coreServiceHelper.Install();
                }
                else
                {
                    await coreServiceHelper.Uninstall();
                }
            }
            catch (Exception e)
            {
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", e.Message);
            }
            
            UseServiceMode = b;
        });
    }

    [Reactive]
    public SystemProxyMode SystemProxyMode { get; set; }

    [Reactive]
    public bool UseServiceMode { get; set; }

    public List<SystemProxyMode> SystemProxyModes { get; }

    public ReactiveCommand<bool, Unit> SetServiceModeCommand { get; }
}