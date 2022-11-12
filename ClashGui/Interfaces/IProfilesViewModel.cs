using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfilesViewModel : IViewModelBase
{
    List<ProfileBase> Profiles { get; }

    ReactiveCommand<ProfileBase, Unit> OpenCreateBox { get; }
}