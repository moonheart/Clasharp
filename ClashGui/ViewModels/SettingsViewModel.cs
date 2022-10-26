using System.Reactive;
using System.Runtime.Serialization;
using ClashGui.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

[DataContract]
public class SettingsViewModel: ViewModelBase, ISettingsViewModel
{
    public SettingsViewModel()
    {
        ClashApiAddress = GlobalConfigs.ControllerApi;
        
        SaveCommand = ReactiveCommand.Create(() =>
        {
            GlobalConfigs.ControllerApi = ClashApiAddress;
        });
    }

    [DataMember]
    [Reactive]
    public string ClashApiAddress { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
}