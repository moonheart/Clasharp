using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Clasharp.Interfaces;
using Clasharp.Models.Profiles;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignProfilesViewModel : ViewModelBase, IProfilesViewModel
{
    public override string Name => "Profiles";
    public ReadOnlyObservableCollection<Profile> Profiles { get; } = new(new(new[] {new Profile()
    {
        Name = "123123", Description = "Desdsfsdf", Type = ProfileType.Local, UpdateTime = DateTime.Now
    }}));

    public Profile? SelectedProfile { get; set; }
    public ReactiveCommand<Profile, Unit> OpenCreateBox { get; } = ReactiveCommand.Create((Profile _) => { });
    public ReactiveCommand<Profile, Unit> DeleteProfile { get; } = ReactiveCommand.Create((Profile _) => { });
}