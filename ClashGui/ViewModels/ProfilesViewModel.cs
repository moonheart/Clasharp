using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.Models.Settings;
using ClashGui.Services;
using DynamicData;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProfilesViewModel : ViewModelBase, IProfilesViewModel
{
    public override string Name => "Profiles";

    public ProfilesViewModel(IProfilesService profilesService, AppSettings appSettings)
    {
        EditProfile = new Interaction<Profile?, Profile?>();
        profilesService.List.ObserveOn(RxApp.MainThreadScheduler).Bind(out _profiles).Subscribe();

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

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<Profile> Profiles => _profiles;

    private ReadOnlyObservableCollection<Profile> _profiles;

    public ReactiveCommand<Profile?, Unit> OpenCreateBox { get; }
}