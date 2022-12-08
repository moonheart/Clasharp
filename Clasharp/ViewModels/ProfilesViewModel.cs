using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using Clasharp.Interfaces;
using Clasharp.Models.Profiles;
using Clasharp.Models.Settings;
using Clasharp.Services;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels;

public class ProfilesViewModel : ViewModelBase, IProfilesViewModel
{
    public override string Name => "Profiles";

    public ProfilesViewModel(IProfilesService profilesService, AppSettings appSettings)
    {
        EditProfile = new Interaction<Profile?, Profile?>();
        profilesService.List.ObserveOn(RxApp.MainThreadScheduler).Bind(out _profiles).Subscribe();
        SelectedProfile = string.IsNullOrWhiteSpace(appSettings.SelectedProfile)
            ? null
            : appSettings.Profiles.FirstOrDefault(d => d.Filename == appSettings.SelectedProfile);

        this.WhenAnyValue(d => d.SelectedProfile)
            .WhereNotNull()
            .Subscribe(d =>
            {
                appSettings.SelectedProfile = d.Filename;
            });
        
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
        DeleteProfile = ReactiveCommand.Create<Profile>(d =>
        {
            profilesService.DeleteProfile(d);
        });
    }

    public Interaction<Profile?, Profile?> EditProfile { get; }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<Profile> Profiles => _profiles;

    [Reactive]
    public Profile? SelectedProfile { get; set; }

    private ReadOnlyObservableCollection<Profile> _profiles;

    public ReactiveCommand<Profile?, Unit> OpenCreateBox { get; }
    public ReactiveCommand<Profile, Unit> DeleteProfile { get; }
}