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
}