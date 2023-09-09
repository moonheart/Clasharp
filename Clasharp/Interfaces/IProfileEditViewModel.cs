using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Clasharp.Models.Profiles;
using ReactiveUI;

namespace Clasharp.Interfaces;

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

    Color TintColor { get; }
}