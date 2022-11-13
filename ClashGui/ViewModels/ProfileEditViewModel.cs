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
    [Reactive]
    public ProfileBase Profile { get; set; }

    public bool IsCreate { get; }
    public List<ProfileType> ProfileTypes { get; }

    [ObservableAsProperty]
    public bool IsRemoteProfile { get; }

    [ObservableAsProperty]
    public bool IsLocalProfile { get; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Notes { get; }
    public string Filename { get; }
    public ProfileType Type { get; set; }
    public string FromFile { get; set; }
    public string RemoteUrl { get; set; }
    public TimeSpan UpdateInterval { get; set; }

    public ReactiveCommand<Unit, ProfileBase> Save { get; }

    public ProfileEditViewModel(ProfileBase? profile)
    {
        var profileType = this.WhenAnyValue(d => d.Profile.Type);
        profileType.Select(d => d == ProfileType.Local).ToPropertyEx(this, d => d.IsLocalProfile);
        profileType.Select(d => d == ProfileType.Remote).ToPropertyEx(this, d => d.IsRemoteProfile);
        
        ProfileTypes = EnumHelper.GetAllEnumValues<ProfileType>().ToList();
        IsCreate = profile == null;
        Profile = profile ?? new LocalProfile();
        Save = ReactiveCommand.CreateFromTask(async d =>
        {
            await SaveProfile();
            return Profile;
        });

        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(d =>
            {
                Debug.WriteLine(Profile.Type);
            });
        this.WhenAnyValue(d => d.Profile.Name)
            .Subscribe(d =>
            {
                Debug.WriteLine(Profile.Name);
            });
    }

    private async Task SaveProfile()
    {
        switch (Profile)
        {
            case RemoteProfile remoteProfile:
                break;
            case LocalProfile localProfile:
                var content = await File.ReadAllTextAsync(localProfile.FromFile);
                var fileName = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.yaml";
                localProfile.Filename = fileName;
                await File.WriteAllTextAsync(fileName, content);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}