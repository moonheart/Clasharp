using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using ReactiveUI;

namespace Clasharp.Utils;

public class NewtonsoftJsonSuspensionDriver<T> : ISuspensionDriver where T : new()
{
    private readonly string _file;

    private readonly JsonSerializerOptions _settings = new()
    {
        WriteIndented = true, IgnoreReadOnlyProperties = true
    };

    public NewtonsoftJsonSuspensionDriver(string file) => _file = file;

    public IObservable<Unit> InvalidateState()
    {
        return Observable.Return(Unit.Default);
    }

    public IObservable<object> LoadState()
    {
        if (!File.Exists(_file))
        {
            return Observable.Return(new T() as object);
        }
        var lines = File.ReadAllText(_file);
        var state = JsonSerializer.Deserialize<T>(lines, _settings);
        return Observable.Return(state ?? new T() as object);
    }

    public IObservable<Unit> SaveState(object state)
    {
        var lines = JsonSerializer.Serialize(state, _settings);
        File.WriteAllText(_file, lines);
        return Observable.Return(Unit.Default);
    }
}