using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClashGui.Clash;
using ClashGui.Clash.Models.Logs;
using ClashGui.Cli.ClashConfigs;
using ReactiveUI;
using Refit;
using YamlDotNet.Serialization;
using LogLevel = ClashGui.Clash.Models.Logs.LogLevel;

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
        _runningState.OnNext(RunningState.Stopped);
    }

    private Regex _logRegex = new Regex(@"\d{2}\:\d{2}\:\d{2}\s+(?<level>\S+)\s*(?<module>\[.+?\])?\s*(?<payload>.+)");

    private Dictionary<string, LogLevel> _levelsMap = new Dictionary<string, LogLevel>()
    {
        ["DBG"] = LogLevel.DEBUG,
        ["INF"] = LogLevel.INFO,
        ["WRN"] = LogLevel.WARNING,
        ["ERR"] = LogLevel.ERROR,
        ["SLT"] = LogLevel.SILENT,
    };

    public async Task<RawConfig> Start()
    {
        _isRunning = RunningState.Starting;
        _runningState.OnNext(RunningState.Starting);

        await EnsureConfig();
        var configYaml = File.ReadAllText(_mainConfig);
        var rawConfig = new DeserializerBuilder().Build().Deserialize<RawConfig>(configYaml);

        _process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _clashExe,
                Arguments = $"-d {_programHome}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = _programHome,
                CreateNoWindow = true,
                UseShellExecute = false,
                StandardOutputEncoding = Encoding.UTF8,
                // Verb = "runas",
            }
        };
        if (rawConfig.Tun?.Enable ?? false)
        {
            _process.StartInfo.Verb = "runas";
        }
        _process.OutputDataReceived += (sender, args) =>
        {
            if (string.IsNullOrEmpty(args.Data)) return;
            var match = _logRegex.Match(args.Data);
            var level = _levelsMap[match.Groups["level"].Value];
            MessageBus.Current.SendMessage(new LogEntry()
            {
                Payload = match.Groups["payload"].Value,
                Type = level
            });
            // args.Data
        };
        _process.Start();
        _process.BeginOutputReadLine();
        var port = (rawConfig.ExternalController ?? "9090").Split(':', StringSplitOptions.RemoveEmptyEntries).Last();

        GlobalConfigs.ClashControllerApi = RestService.For<IClashControllerApi>($"http://localhost:{port}", new RefitSettings()
        {
            ExceptionFactory = message => Task.FromResult<Exception?>(null)
        });

        if (_process.WaitForExit(1000))
        {
            throw new Exception(_process.StandardError.ReadToEnd());
        }
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