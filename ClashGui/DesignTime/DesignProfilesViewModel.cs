using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProfilesViewModel : ViewModelBase, IProfilesViewModel
{
    public override string Name => "Profiles";
    public ReadOnlyObservableCollection<Profile> Profiles { get; }
    public ReactiveCommand<Profile, Unit> OpenCreateBox { get; }
}