using System;
using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfileEditViewModel : IViewModelBase
{
    FullEditProfile Profile { get; set; }
    ProfileType ProfileType { get; set; }

    ReactiveCommand<Unit, ProfileBase?> Save { get; }

    bool IsCreate { get; }

    List<ProfileType> ProfileTypes { get; }

    bool IsRemoteProfile { get; }
    bool IsLocalProfile { get; }

}