using System;
using System.Diagnostics;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace ClashGui.Cli;

public interface IClashCli
{
    IObservable<RunningState> Running { get; }

    Task Start();
}

public enum RunningState
{
    Stopped,
    Starting,
    Started,
    Stopping
}

public class ClashCli: IClashCli
{
    private Process? _process;

    private static string _userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    private static string _programHome = Path.Combine(_userhome, ".config", "clashgui");
    private static string _mainConfig = Path.Combine(_programHome, "config.yaml");
    private static string _clashExe = Path.Combine(_programHome, "clash-windows-amd64.exe");

    public IObservable<RunningState> Running => _isRunning;

    private readonly ISubject<RunningState> _isRunning = new ReplaySubject<RunningState>();

    public ClashCli()
    {
        _isRunning.OnNext(RunningState.Stopped);
    }

    public async Task Start()
    {
        _isRunning.OnNext(RunningState.Starting);

        await EnsureConfig();

        _process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _clashExe,
                Arguments = $"-d {_programHome}",
                WorkingDirectory = _programHome,
                Verb = "runas",
            }
        };
        _process.Start();
        _isRunning.OnNext(RunningState.Started);
    }

    private async Task EnsureConfig()
    {
        Directory.CreateDirectory(_programHome);
        if (!File.Exists(_mainConfig))
        {
            await File.WriteAllTextAsync(_mainConfig,
                @"mixed-port: 17890
allow-lan: false
external-controller: 0.0.0.0:62708");
        }
    }
}