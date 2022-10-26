using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignSettingsViewModel:ViewModelBase, ISettingsViewModel
{
    public string ClashApiAddress { get; set; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
}