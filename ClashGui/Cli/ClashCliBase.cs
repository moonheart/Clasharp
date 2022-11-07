using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Logs;
using ClashGui.Cli.ClashConfigs;
using ClashGui.Common;
using ClashGui.Services;
using YamlDotNet.Serialization;
using LogLevel = ClashGui.Clash.Models.Logs.LogLevel;

namespace ClashGui.Cli;

public abstract class ClashCliBase : IClashCli
{
    public IObservable<RawConfig> Config => _config;
    public IObservable<RunningState> RunningState => _runningState;
    public IObservable<LogEntry> ConsoleLog => _consoleLog;

    protected ReplaySubject<RunningState> _runningState = new(1);
    protected ReplaySubject<LogEntry> _consoleLog = new(1);
    protected ReplaySubject<RawConfig> _config = new(1);

    protected IClashApiFactory _clashApiFactory;

    protected Regex _logRegex = new(@"\d{2}\:\d{2}\:\d{2}\s+(?<level>\S+)\s*(?<module>\[.+?\])?\s*(?<payload>.+)");
    protected Regex _logMetaRegex = new(@"time=""(.+?) level=(?<level>.+?) msg=""(?<payload>.+)""");


    protected Dictionary<string, LogLevel> _levelsMap = new()
    {
        ["DBG"] = LogLevel.DEBUG,
        ["INF"] = LogLevel.INFO,
        ["WRN"] = LogLevel.WARNING,
        ["ERR"] = LogLevel.ERROR,
        ["SLT"] = LogLevel.SILENT,
        ["debug"] = LogLevel.DEBUG,
        ["info"] = LogLevel.INFO,
        ["warning"] = LogLevel.WARNING,
        ["error"] = LogLevel.ERROR,
        ["silent"] = LogLevel.SILENT,
    };

    public async Task Start()
    {
        _runningState.OnNext(Cli.RunningState.Starting);
        await EnsureConfig();
        var configYaml = await File.ReadAllTextAsync(GlobalConfigs.ClashConfig);
        var rawConfig = new DeserializerBuilder().Build().Deserialize<RawConfig>(configYaml);

        await DoStart();
        
        var port = (rawConfig.ExternalController ?? "9090").Split(':', StringSplitOptions.RemoveEmptyEntries).Last();
        _clashApiFactory.SetPort(int.Parse(port));
        _config.OnNext(rawConfig);
        _runningState.OnNext(Cli.RunningState.Started);
    }

    protected abstract Task DoStart();

    public async Task Stop()
    {
        _runningState.OnNext(Cli.RunningState.Stopping);
        await DoStop();
        _runningState.OnNext(Cli.RunningState.Stopped);
    }
    protected abstract Task DoStop();

    private async Task EnsureConfig()
    {
        Directory.CreateDirectory(GlobalConfigs.ProgramHome);
        if (!File.Exists(GlobalConfigs.ClashConfig))
        {
            await File.WriteAllTextAsync(GlobalConfigs.ClashConfig,
                @"mixed-port: 17890
allow-lan: false
external-controller: 0.0.0.0:62708");
        }
    }
}