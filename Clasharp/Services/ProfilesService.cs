using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Logs;
using Clasharp.Models.Profiles;
using Clasharp.Models.Settings;
using Clasharp.Services.Base;
using Clasharp.Common;
using DynamicData;
using ReactiveUI;

namespace Clasharp.Services;

public interface IProfilesService : IObservableListService<Profile, string>, IAutoFreshable
{
    void AddProfile(Profile profile);

    void DeleteProfile(Profile profile);

    void ReplaceProfile(Profile old, Profile newp);

    string? GetActiveProfile();

    Task DownloadProfile(Profile profile);
}

public class ProfilesService : IDisposable, IProfilesService
{
    public IObservable<IChangeSet<Profile, string>> List => _profiles.Connect();
    public bool EnableAutoFresh { get; set; }

    private Dictionary<string, (int, IDisposable)> _profileAutoUpdates = new();

    private readonly SourceCache<Profile, string> _profiles;
    private FileSystemWatcher _fileSystemWatcher;

    private AppSettings _appSettings;

    public ProfilesService(AppSettings appSettings)
    {
        _profiles = new(d => d.Filename);

        if (!Directory.Exists(GlobalConfigs.ProfilesDir)) Directory.CreateDirectory(GlobalConfigs.ProfilesDir);

        _appSettings = appSettings;

        _fileSystemWatcher = new FileSystemWatcher(GlobalConfigs.ProfilesDir, "*.yaml");
        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
        _fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
        _fileSystemWatcher.Created += FileSystemWatcherOnChanged;
        _fileSystemWatcher.Deleted += FileSystemWatcherOnChanged;
        _fileSystemWatcher.Renamed += FileSystemWatcherOnChanged;
        _fileSystemWatcher.EnableRaisingEvents = false;

        FileSystemWatcherOnChanged(null!, null!);
    }

    private void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs e)
    {
        foreach (var profile in _appSettings.Profiles)
        {
            if (profile.Type == ProfileType.Remote && profile.UpdateInterval != null)
            {
                if (_profileAutoUpdates.TryGetValue(profile.Filename, out var value))
                {
                    if (value.Item1 != profile.UpdateInterval)
                    {
                        _profileAutoUpdates[profile.Filename].Item2.Dispose();
                        SetupInterval(profile);
                    }
                }
                else
                {
                    SetupInterval(profile);
                }
            }

            var fullPath = Path.Combine(GlobalConfigs.ProfilesDir, profile.Filename);
            var fileInfo = new FileInfo(fullPath);
            if (!fileInfo.Exists)
            {
                profile.Notes = "file missing";
            }
            else
            {
                profile.Notes = "";
                profile.CreateTime = fileInfo.CreationTime;
                profile.UpdateTime = fileInfo.LastWriteTime;
            }
        }

        _profiles.AddOrUpdate(_appSettings.Profiles);
    }

    private void SetupInterval(Profile profile)
    {
        async void OnNext(long _)
        {
            await DownloadProfile(profile);
        }

        var disposable = Observable.Interval(TimeSpan.FromMinutes((double) profile.UpdateInterval!)).Subscribe(OnNext);
        _profileAutoUpdates[profile.Filename!] = (profile.UpdateInterval.Value, disposable);
    }

    public void Dispose()
    {
        _fileSystemWatcher.Dispose();
        foreach (var (_, (_, disposable)) in _profileAutoUpdates)
        {
            disposable.Dispose();
        }
    }

    public void AddProfile(Profile profile)
    {
        _appSettings.Profiles.Add(profile);
        _profiles.AddOrUpdate(profile);
    }

    public void DeleteProfile(Profile profile)
    {
        _appSettings.Profiles.Remove(profile);
        _profiles.Remove(profile);
        var fullPath = Path.Combine(GlobalConfigs.ProfilesDir, profile.Filename);
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }

    public void ReplaceProfile(Profile old, Profile newp)
    {
        _appSettings.Profiles.Replace(old, newp);
        _profiles.AddOrUpdate(newp);
    }

    private static HttpClient _httpClient = new();

    public async Task DownloadProfile(Profile profile)
    {
        try
        {
            var content = await _httpClient.GetStringAsync(profile.RemoteUrl);
            await File.WriteAllTextAsync(Path.Combine(GlobalConfigs.ProfilesDir, profile.Filename), content);
            profile.UpdateTime = DateTime.Now;
        }
        catch (Exception e)
        {
            MessageBus.Current.SendMessage(new LogEntry(LogLevel.ERROR,
                $"Failed to download profile from {profile.RemoteUrl}, {e.Message}"));
        }
    }

    public string? GetActiveProfile()
    {
        return _appSettings.SelectedProfile;
    }
}