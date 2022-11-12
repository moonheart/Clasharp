using System.Collections.Generic;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfileEditViewModel:IViewModelBase
{
    ProfileBase Profile { get; set; }

    ReactiveCommand<Unit, ProfileBase> Save { get; }
    
    bool IsCreate { get; }
    
    List<ProfileType> ProfileTypes { get; }
}