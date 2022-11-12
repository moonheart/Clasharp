using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.Models.Settings;
using ClashGui.Services;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProfilesViewModel : ViewModelBase, IProfilesViewModel
{
    public override string Name => "Profiles";

    public ProfilesViewModel(IProfilesService profilesService, AppSettings appSettings)
    {
        EditProfile = new Interaction<ProfileBase?, ProfileBase?>();
        profilesService.Obj.ToPropertyEx(this, d => d.Profiles);

        OpenCreateBox = ReactiveCommand.CreateFromTask<ProfileBase?>(async d =>
        {
            if (d == null)
            {
                var newProfile = await EditProfile.Handle(null);
                if (newProfile != null)
                {
                    appSettings.Profiles.Add(newProfile);
                }
            }
            else
            {
                var profileBase = JsonSerializer.SerializeToDocument(d).Deserialize<ProfileBase>();
                var newProfile = await EditProfile.Handle(profileBase);
                if (newProfile != null)
                {
                    appSettings.Profiles.Replace(d, newProfile);
                }
            }
        });
    }

    public Interaction<ProfileBase?, ProfileBase?> EditProfile { get; }

    [ObservableAsProperty]
    public List<ProfileBase> Profiles { get; }

    public ReactiveCommand<ProfileBase?, Unit> OpenCreateBox { get; }
}