using System;
using System.Collections.Generic;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.DesignTime;

public class DesignProfileEditViewModel:ViewModelBase, IProfileEditViewModel
{
    public ProfileBase Profile { get; set; }
    public ReactiveCommand<Unit, ProfileBase> Save { get; }
    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; }
    public bool IsRemoteProfile { get; }
    public bool IsLocalProfile { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Notes { get; }
    public string Filename { get; }
    public ProfileType Type { get; set; }
    public string FromFile { get; set; }
    public string RemoteUrl { get; set; }
    public TimeSpan UpdateInterval { get; set; }
}