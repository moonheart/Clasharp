using System.Reactive;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface ISettingsViewModel: IViewModelBase
{
    string ClashApiAddress { get; set; }
    
    ReactiveCommand<Unit, Unit> SaveCommand { get; }
}