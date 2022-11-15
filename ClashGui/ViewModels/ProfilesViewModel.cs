using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.Models.Settings;
using ClashGui.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProfilesViewModel : ViewModelBase, IProfilesViewModel
{
    public override string Name => "Profiles";

    public ProfilesViewModel(IProfilesService profilesService, AppSettings appSettings)
    {
        EditProfile = new Interaction<Profile?, Profile?>();
        profilesService.Obj.ToPropertyEx(this, d => d.Profiles);

        OpenCreateBox = ReactiveCommand.CreateFromTask<Profile?>(async d =>
        {
            if (d == null)
            {
                var newProfile = await EditProfile.Handle(null);
                if (newProfile != null)
                {
                    profilesService.AddProfile(newProfile);
                }
            }
            else
            {
                var profileBase = JsonSerializer.SerializeToDocument(d).Deserialize<Profile>();
                var newProfile = await EditProfile.Handle(profileBase);
                if (newProfile != null)
                {
                    profilesService.ReplaceProfile(d, newProfile);
                }
            }
        });
    }

    public Interaction<Profile?, Profile?> EditProfile { get; }

    [ObservableAsProperty]
    public List<Profile> Profiles { get; }

    public ReactiveCommand<Profile?, Unit> OpenCreateBox { get; }
}