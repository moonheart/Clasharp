using System;
using System.Collections.Generic;
using System.Reactive;
using Clasharp.Interfaces;
using Clasharp.Models.Profiles;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignProfileEditViewModel:ViewModelBase, IProfileEditViewModel
{
    public Profile Profile { get; set; }
    public ProfileType ProfileType { get; set; }
    public string FromFile { get; set; }
    public ReactiveCommand<Unit, Profile> Save { get; }
    public ReactiveCommand<Unit, Unit> OpenFile { get; }
    public Interaction<Unit, string?> ShowOpenFileDialog { get; }
    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; }
    public bool IsRemoteProfile { get; }
    public bool IsLocalProfile { get; }
}