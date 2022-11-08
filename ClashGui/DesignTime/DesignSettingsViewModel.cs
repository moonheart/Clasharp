using System.Collections.Generic;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.Models.Settings;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignSettingsViewModel:ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";
    public string ClashApiAddress { get; set; }
    public SystemProxyMode SystemProxyMode { get; set; }
    public bool UseServiceMode { get; set; }
    public List<SystemProxyMode> SystemProxyModes { get; }
    public ReactiveCommand<bool, Unit> SetServiceModeCommand { get; }
}