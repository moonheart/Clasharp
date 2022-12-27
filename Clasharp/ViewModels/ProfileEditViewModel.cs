using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Themes.Fluent;
using Clasharp.Interfaces;
using Clasharp.Models.Profiles;
using Clasharp.Services;
using Clasharp.Utils;
using Clasharp.Common;
using Clasharp.Models.Settings;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels;

public class ProfileEditViewModel : ViewModelBase, IProfileEditViewModel
{
    public Profile Profile { get; set; }

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

    public string FromFile
    {
        get => Profile.FromFile;
        set
        {
            this.RaisePropertyChanging();
            if (Profile.FromFile == value)
            {
                return;
            }
            Profile.FromFile = value;
            this.RaisePropertyChanged();
        }
    }
    public ReactiveCommand<Unit, Unit> OpenFile { get; }
    public Interaction<Unit, string?> ShowOpenFileDialog { get; }
    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; }

    [ObservableAsProperty]
    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public bool IsRemoteProfile { get; }

    [ObservableAsProperty]
    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public bool IsLocalProfile { get; }

    [ObservableAsProperty]
    public Color TintColor { get; }

    public ReactiveCommand<Unit, Profile?> Save { get; }
    private Profile? _profileBase;

    private IProfilesService _profilesService;

    public ProfileEditViewModel(Profile? profile, IProfilesService profilesService, AppSettings appSettings)
    {
        _profileBase = profile;
        _profilesService = profilesService;
        ProfileTypes = EnumHelper.GetAllEnumValues<ProfileType>().ToList();
        IsCreate = profile == null;
        Profile = new Profile
        {
            Name = profile?.Name,
            Description = profile?.Description,
            FromFile = profile?.FromFile,
            RemoteUrl = profile?.RemoteUrl,
            UpdateInterval = profile?.UpdateInterval,
            Type = profile?.Type ?? ProfileType.Local
        };

        var profileType = this.WhenAnyValue(d => d.ProfileType);
        profileType.Select(d => d == ProfileType.Local).ToPropertyEx(this, d => d.IsLocalProfile);
        profileType.Select(d => d == ProfileType.Remote).ToPropertyEx(this, d => d.IsRemoteProfile);

        Save = ReactiveCommand.CreateFromTask(async _ => await SaveProfile());
        ShowOpenFileDialog = new Interaction<Unit, string?>();
        OpenFile = ReactiveCommand.CreateFromTask(async () =>
        {
            var filename = await ShowOpenFileDialog.Handle(Unit.Default);
            if (filename != null)
            {
                FromFile = filename;
            }
        });
        appSettings.WhenAnyValue(d => d.ThemeMode)
            .Select(d => d == FluentThemeMode.Dark ? Colors.Black : Colors.White)
            .ToPropertyEx(this, d => d.TintColor);

    }

    private async Task<Profile?> SaveProfile()
    {
        switch (Profile.Type)
        {
            case ProfileType.Remote:
                if (IsCreate)
                {
                    var profile = new Profile
                    {
                        Name = Profile.Name,
                        Description = Profile.Description,
                        Filename = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.yaml",
                        Type = ProfileType.Remote,
                        CreateTime = DateTime.Now,
                        RemoteUrl = Profile.RemoteUrl,
                        UpdateInterval = Profile.UpdateInterval,
                    };
                    await _profilesService.DownloadProfile(profile);
                    return profile;
                }

                _profileBase!.Name = Profile.Name;
                _profileBase!.Description = Profile.Description;
                _profileBase.RemoteUrl = Profile.RemoteUrl;
                _profileBase.UpdateInterval = Profile.UpdateInterval;

                return _profileBase;
            case ProfileType.Local:
                if (IsCreate)
                {
                    if (string.IsNullOrWhiteSpace(Profile.FromFile))
                    {
                        return null;
                    }

                    var content = await File.ReadAllTextAsync(Profile.FromFile);
                    var fileName = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.yaml";
                    await File.WriteAllTextAsync(Path.Combine(GlobalConfigs.ProfilesDir, fileName), content);
                    return new Profile()
                    {
                        Name = Profile.Name,
                        Type = ProfileType.Local,
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