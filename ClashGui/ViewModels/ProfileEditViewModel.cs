using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProfileEditViewModel:ViewModelBase, IProfileEditViewModel
{
    [Reactive]
    public ProfileBase Profile { get; set; }
    
    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; }
    public ReactiveCommand<Unit, ProfileBase> Save { get; }

    public ProfileEditViewModel(ProfileBase? profile)
    {
        ProfileTypes = EnumHelper.GetAllEnumValues<ProfileType>().ToList();
        IsCreate = profile == null;
        Profile = profile ?? new LocalProfile();
        Save = ReactiveCommand.CreateFromTask(async d =>
        {
            return Profile;
        });
        
    }
}