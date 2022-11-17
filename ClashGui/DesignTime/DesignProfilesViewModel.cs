using System;
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
    public ReadOnlyObservableCollection<Profile> Profiles { get; } = new(new(new[] {new Profile()
    {
        Name = "123123", Description = "Desdsfsdf", Type = ProfileType.Local, UpdateTime = DateTime.Now
    }}));
    public ReactiveCommand<Profile, Unit> OpenCreateBox { get; }
}