using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ClashGui.Clash;
using ClashGui.Cli.ClashConfigs;
using Refit;
using YamlDotNet.Serialization;

namespace ClashGui.Cli;

public interface IClashCli
{
    RunningState Running { get; }
    
    IObservable<RunningState> RunningObservable { get; }

    Task<RawConfig> Start();
    Task Stop();
}

public enum RunningState
{
    Stopped,
    Starting,
    Started,
    Stopping
}

public class ClashCli : IClashCli
{
    private Process? _process;

    private static string _userhome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    private static string _programHome = Path.Combine(_userhome, ".config", "clashgui");
    private static string _mainConfig = Path.Combine(_programHome, "config.yaml");
    private static string _clashExe = Path.Combine(_programHome, "clash-windows-amd64.exe");

    public RunningState Running => _isRunning;
    public IObservable<RunningState> RunningObservable => _runningState;

    private RunningState _isRunning;
    private ReplaySubject<RunningState> _runningState = new ReplaySubject<RunningState>(1);

    public ClashCli()
    {
        _isRunning = RunningState.Stopped;
    }

    public async Task<RawConfig> Start()
    {
        _isRunning = RunningState.Starting;
        _runningState.OnNext(RunningState.Starting);

        await EnsureConfig();

        _process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _clashExe,
                Arguments = $"-d {_programHome}",
                WorkingDirectory = _programHome,
                // Verb = "runas",
            }
        };
        _process.Start();
        var configYaml = File.ReadAllText(_mainConfig);
        var rawConfig = new DeserializerBuilder().Build().Deserialize<RawConfig>(configYaml);

        GlobalConfigs.ClashControllerApi = RestService.For<IClashControllerApi>($"http://localhost:{rawConfig.ExternalController.Split(':', StringSplitOptions.RemoveEmptyEntries).Last()}", new RefitSettings()
        {
            ExceptionFactory = message => Task.FromResult<Exception?>(null)
        });
        
        _isRunning = RunningState.Started;
        _runningState.OnNext(RunningState.Started);
        return rawConfig;
    }

    public Task Stop()
    {
        _isRunning = RunningState.Stopping;
        _runningState.OnNext(RunningState.Stopping);
        _process?.Kill(true);
        _isRunning = RunningState.Stopped;
        _runningState.OnNext(RunningState.Stopped);
        return Task.CompletedTask;
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