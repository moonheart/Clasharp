using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfilesViewModel : IViewModelBase
{
    List<Profile> Profiles { get; }

    ReactiveCommand<Profile, Unit> OpenCreateBox { get; }
}