using System;
using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfileEditViewModel : IViewModelBase
{
    ProfileBase Profile { get; set; }

    ReactiveCommand<Unit, ProfileBase> Save { get; }

    bool IsCreate { get; }

    List<ProfileType> ProfileTypes { get; }

    bool IsRemoteProfile { get; }
    bool IsLocalProfile { get; }

    string Name { get; set; }
    string Description { get; set; }
    string Notes { get; }
    string Filename { get; }
    ProfileType Type { get; set; }
    string FromFile { get; set; }
    string RemoteUrl { get; set; }
    TimeSpan UpdateInterval { get; set; }
}