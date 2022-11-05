using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ClashGui.Interfaces;
using ClashGui.Models.Settings;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class SettingsViewModel: ViewModelBase, ISettingsViewModel
{
    public override string Name => "Settings";

    public SettingsViewModel()
    {
        SystemProxyModes = EnumHelper.GetAllEnumValues<SystemProxyMode>().ToList();
    }

    [DataMember]
    [Reactive]
    public string ClashApiAddress { get; set; }
    
    [DataMember]
    [Reactive]
    public SystemProxyMode SystemProxyMode { get; set; }

    [JsonIgnore]
    public List<SystemProxyMode> SystemProxyModes { get; }

    [JsonIgnore]
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
}