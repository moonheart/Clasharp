using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Settings;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface ISettingsViewModel: IViewModelBase
{
    SystemProxyMode SystemProxyMode { get; set; }
    bool UseServiceMode { get; set; }
    List<SystemProxyMode> SystemProxyModes { get; }
    
    ReactiveCommand<bool, Unit> SetServiceModeCommand { get; }
}