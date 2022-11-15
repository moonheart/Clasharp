using System;
using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfileEditViewModel : IViewModelBase
{
    Profile Profile { get; set; }
    ProfileType ProfileType { get; set; }
    string FromFile { get; set; }
    ReactiveCommand<Unit, Profile?> Save { get; }
    ReactiveCommand<Unit, Unit> OpenFile { get; }
    Interaction<Unit, string?> ShowOpenFileDialog { get; }

    bool IsCreate { get; }

    List<ProfileType> ProfileTypes { get; }

    bool IsRemoteProfile { get; }
    bool IsLocalProfile { get; }

}