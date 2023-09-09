using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media;
using Clasharp.Interfaces;
using Clasharp.Models.Profiles;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignProfileEditViewModel:ViewModelBase, IProfileEditViewModel
{
    public Profile Profile { get; set; } = new();
    public ProfileType ProfileType { get; set; }
    public string FromFile { get; set; } = string.Empty;
    public ReactiveCommand<Unit, Profile> Save { get; } = ReactiveCommand.Create(() => new Profile());
    public ReactiveCommand<Unit, Unit> OpenFile { get; } = ReactiveCommand.Create(() => { });
    public Interaction<Unit, string?> ShowOpenFileDialog { get; } = new();
    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; } = new();
    public bool IsRemoteProfile { get; }
    public bool IsLocalProfile { get; }
    public Color TintColor { get; } = Colors.Transparent;
}