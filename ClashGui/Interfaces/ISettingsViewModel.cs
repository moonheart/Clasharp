using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Settings;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface ISettingsViewModel: IViewModelBase
{
    SystemProxyMode SystemProxyMode { get; set; }
    
    List<SystemProxyMode> SystemProxyModes { get; }
    
    ReactiveCommand<Unit, Unit> SaveCommand { get; }
}