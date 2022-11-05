using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Utils;

public class NewtonsoftJsonSuspensionDriver : ISuspensionDriver
{
    private readonly string _file;

    private readonly JsonSerializerOptions _settings = new JsonSerializerOptions
    {
    };

    public NewtonsoftJsonSuspensionDriver(string file) => _file = file;

    public IObservable<Unit> InvalidateState()
    {
        if (File.Exists(_file))
            File.Delete(_file);
        return Observable.Return(Unit.Default);
    }

    public IObservable<object> LoadState()
    {
        if (!File.Exists(_file))
        {
            return Observable.Return(new SettingsViewModel());
        }
        var lines = File.ReadAllText(_file);
        var state = JsonSerializer.Deserialize<SettingsViewModel>(lines, _settings);
        return Observable.Return(state ?? new SettingsViewModel());
    }

    public IObservable<Unit> SaveState(object state)
    {
        var lines = JsonSerializer.Serialize(state, _settings);
        File.WriteAllText(_file, lines);
        return Observable.Return(Unit.Default);
    }
}