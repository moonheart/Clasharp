using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using ClashGui.Common;
using ClashGui.Models.Profiles;
using ClashGui.Models.Settings;
using ClashGui.Services.Base;
using DynamicData;

namespace ClashGui.Services;

public interface IProfilesService : IObservalbeObjService<List<Profile>>, IAutoFreshable
{
    void AddProfile(Profile profile);

    void ReplaceProfile(Profile old, Profile newp);
}

public class ProfilesService : IDisposable, IProfilesService
{
    public IObservable<List<Profile>> Obj => _profiles;
    public bool EnableAutoFresh { get; set; }

    private ReplaySubject<List<Profile>> _profiles = new();
    private FileSystemWatcher _fileSystemWatcher;

    private AppSettings _appSettings;

    public ProfilesService(AppSettings appSettings)
    {
        if (!Directory.Exists(GlobalConfigs.ProfilesDir)) Directory.CreateDirectory(GlobalConfigs.ProfilesDir);

        _appSettings = appSettings;

        _fileSystemWatcher = new FileSystemWatcher(GlobalConfigs.ProfilesDir, "*.yaml");
        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
        _fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
        _fileSystemWatcher.Created += FileSystemWatcherOnChanged;
        _fileSystemWatcher.Deleted += FileSystemWatcherOnChanged;
        _fileSystemWatcher.Renamed += FileSystemWatcherOnChanged;
        _fileSystemWatcher.EnableRaisingEvents = true;
        
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
        
        _profiles.OnNext(_appSettings.Profiles);
    }

    public void Dispose()
    {
        _fileSystemWatcher.Dispose();
    }

    public void AddProfile(Profile profile)
    {
        _appSettings.Profiles.Add(profile);
        _profiles.OnNext(_appSettings.Profiles);
    }

    public void ReplaceProfile(Profile old, Profile newp)
    {
        _appSettings.Profiles.Replace(old, newp);
        _profiles.OnNext(_appSettings.Profiles);
    }
}