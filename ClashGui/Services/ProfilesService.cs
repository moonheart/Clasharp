using System;
using System.IO;
using ClashGui.Common;
using ClashGui.Models.Profiles;
using ClashGui.Models.Settings;
using ClashGui.Services.Base;
using DynamicData;

namespace ClashGui.Services;

public interface IProfilesService : IObservableListService<Profile, string>, IAutoFreshable
{
    void AddProfile(Profile profile);

    void ReplaceProfile(Profile old, Profile newp);

    string? GetActiveProfile();
}

public class ProfilesService : IDisposable, IProfilesService
{
    public IObservable<IChangeSet<Profile, string>> List => _profiles.Connect();
    public bool EnableAutoFresh { get; set; }

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
            var fullPath = Path.Combine(GlobalConfigs.ProfilesDir, profile.Filename);
            var fileInfo = new FileInfo(fullPath);
            if (!fileInfo.Exists)
            {
                profile.Notes = "file missing";
            }
            else
            {
                profile.CreateTime = fileInfo.CreationTime;
                profile.UpdateTime = fileInfo.LastWriteTime;
            }
        }

        _profiles.AddOrUpdate(_appSettings.Profiles);
    }

    public void Dispose()
    {
        _fileSystemWatcher.Dispose();
    }

    public void AddProfile(Profile profile)
    {
        _appSettings.Profiles.Add(profile);
        _profiles.AddOrUpdate(profile);
    }

    public void ReplaceProfile(Profile old, Profile newp)
    {
        _appSettings.Profiles.Replace(old, newp);
        _profiles.AddOrUpdate(newp);
    }

    public string? GetActiveProfile()
    {
        return _appSettings.SelectedProfile;
    }
}