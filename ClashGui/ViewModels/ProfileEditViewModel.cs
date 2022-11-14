using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ProfileEditViewModel : ViewModelBase, IProfileEditViewModel
{
    public FullEditProfile Profile { get; set; }

    public ProfileType ProfileType
    {
        get => Profile.Type;
        set
        {
            this.RaisePropertyChanging();
            if (Profile.Type == value)
            {
                return;
            }
            Profile.Type = value;
            this.RaisePropertyChanged();
        }
    }

    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; }

    [ObservableAsProperty]
    public bool IsRemoteProfile { get; }

    [ObservableAsProperty]
    public bool IsLocalProfile { get; }

    public ReactiveCommand<Unit, ProfileBase?> Save { get; }
    private ProfileBase? _profileBase;

    public ProfileEditViewModel(ProfileBase? profile)
    {
        _profileBase = profile;
        ProfileTypes = EnumHelper.GetAllEnumValues<ProfileType>().ToList();
        IsCreate = profile == null;
        Profile = new FullEditProfile()
        {
            Name = profile?.Name,
            Description = profile?.Description,
        };
        switch (profile)
        {
            case LocalProfile localProfile:
                Profile.FromFile = localProfile.FromFile;
                break;
            case RemoteProfile remoteProfile:
                Profile.RemoteUrl = remoteProfile.RemoteUrl;
                Profile.UpdateInterval = remoteProfile.UpdateInterval;
                break;
        }

        var profileType = this.WhenAnyValue(d => d.ProfileType);
        profileType.Select(d => d == ProfileType.Local).ToPropertyEx(this, d => d.IsLocalProfile);
        profileType.Select(d => d == ProfileType.Remote).ToPropertyEx(this, d => d.IsRemoteProfile);
        profileType.Subscribe(d => { });
        this.WhenAnyValue(d => d.Profile.Name).Subscribe(d => { });

        Save = ReactiveCommand.CreateFromTask(async d => { return await SaveProfile(); });
    }

    private async Task<ProfileBase?> SaveProfile()
    {
        switch (Profile.Type)
        {
            case ProfileType.Remote:
                break;
            case ProfileType.Local:
                if (IsCreate)
                {
                    var content = await File.ReadAllTextAsync(Profile.FromFile);
                    var fileName = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.yaml";
                    await File.WriteAllTextAsync(fileName, content);
                    return new LocalProfile
                    {
                        Name = Profile.Name,
                        Description = Profile.Description,
                        Filename = fileName,
                        CreateTime = DateTime.Now
                    };
                }

                _profileBase!.Name = Profile.Name;
                _profileBase!.Description = Profile.Description;
                return _profileBase;
        }

        return null;
    }
}