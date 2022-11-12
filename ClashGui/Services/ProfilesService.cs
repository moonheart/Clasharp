using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using ClashGui.Common;
using ClashGui.Models.Profiles;
using ClashGui.Models.Settings;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IProfilesService : IObservalbeObjService<List<ProfileBase>>, IAutoFreshable
{
}

public class ProfilesService : IDisposable, IProfilesService
{
    public IObservable<List<ProfileBase>> Obj => _profiles;
    public bool EnableAutoFresh { get; set; }

    private AsyncSubject<List<ProfileBase>> _profiles = new();
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
}