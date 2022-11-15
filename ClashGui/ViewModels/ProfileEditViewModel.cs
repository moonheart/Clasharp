using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Common;
using ClashGui.Interfaces;
using ClashGui.Models.Profiles;
using ClashGui.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

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
    public bool IsRemoteProfile { get; }

    [ObservableAsProperty]
    public bool IsLocalProfile { get; }

    public ReactiveCommand<Unit, Profile?> Save { get; }
    private Profile? _profileBase;

    public ProfileEditViewModel(Profile? profile)
    {
        _profileBase = profile;
        ProfileTypes = EnumHelper.GetAllEnumValues<ProfileType>().ToList();
        IsCreate = profile == null;
        Profile = new Profile
        {
            Name = profile?.Name,
            Description = profile?.Description,
            FromFile = profile?.FromFile,
            RemoteUrl = profile?.RemoteUrl,
            UpdateInterval = profile?.UpdateInterval,
        };

        var profileType = this.WhenAnyValue(d => d.ProfileType);
        profileType.Select(d => d == ProfileType.Local).ToPropertyEx(this, d => d.IsLocalProfile);
        profileType.Select(d => d == ProfileType.Remote).ToPropertyEx(this, d => d.IsRemoteProfile);
        profileType.Subscribe(d => { });
        this.WhenAnyValue(d => d.Profile.Name).Subscribe(d => { });

        Save = ReactiveCommand.CreateFromTask(async d => { return await SaveProfile(); });
        ShowOpenFileDialog = new Interaction<Unit, string?>();
        OpenFile = ReactiveCommand.CreateFromTask(async () =>
        {
            var filename = await ShowOpenFileDialog.Handle(Unit.Default);
            if (filename != null)
            {
                FromFile = filename;
            }
        });
    }

    private static HttpClient _httpClient = new();
    private async Task<Profile?> SaveProfile()
    {
        switch (Profile.Type)
        {
            case ProfileType.Remote:
                if (IsCreate)
                {
                    var content = await _httpClient.GetStringAsync(Profile.RemoteUrl);
                    var fileName = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.yaml";
                    await File.WriteAllTextAsync(fileName, content);
                    return new Profile
                    {
                        Name = Profile.Name,
                        Description = Profile.Description,
                        Filename = fileName,
                        Type = ProfileType.Remote,
                        CreateTime = DateTime.Now,
                        RemoteUrl = Profile.RemoteUrl,
                        UpdateInterval = Profile.UpdateInterval,
                    };
                }

                _profileBase!.Name = Profile.Name;
                _profileBase!.Description = Profile.Description;
                _profileBase.RemoteUrl = Profile.RemoteUrl;
                _profileBase.UpdateInterval = Profile.UpdateInterval;

                return _profileBase;
            case ProfileType.Local:
                if (IsCreate)
                {
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