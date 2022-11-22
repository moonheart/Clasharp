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
using ClashGui.Models.Settings;
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


    protected Regex _logRegex = new(@"\d{2}\:\d{2}\:\d{2}\s+(?<level>\S+)\s*(?<module>\[.+?\])?\s*(?<payload>.+)");
    protected Regex _logMetaRegex = new(@"time=""(.+?) level=(?<level>.+?) msg=""(?<payload>.+)""");

    protected IClashApiFactory _clashApiFactory;
    private IProfilesService _profilesService;
    private AppSettings _appSettings;

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

    protected ClashCliBase(IClashApiFactory clashApiFactory, IProfilesService profilesService, AppSettings appSettings)
    {
        _clashApiFactory = clashApiFactory;
        _profilesService = profilesService;
        _appSettings = appSettings;
    }

    public async Task Start()
    {
        _runningState.OnNext(Cli.RunningState.Starting);
        var configYaml = await File.ReadAllTextAsync(await EnsureConfig());
        var rawConfig = new DeserializerBuilder().Build().Deserialize<RawConfig>(configYaml);
        MergeManagedFields(rawConfig);
        var mergedYaml = new SerializerBuilder().ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).Build().Serialize(rawConfig);
        await File.WriteAllTextAsync(GlobalConfigs.RuntimeClashConfig, mergedYaml);

        var clashWrapper = new ClashWrapper(new ClashLaunchInfo
        {
            ConfigPath = GlobalConfigs.RuntimeClashConfig, ExecutablePath = GlobalConfigs.ClashExe, WorkDir = GlobalConfigs.ProgramHome
        });
        clashWrapper.Test();

        await DoStart(GlobalConfigs.RuntimeClashConfig);

        var port = (rawConfig.ExternalController ?? "9090").Split(':', StringSplitOptions.RemoveEmptyEntries).Last();
        _clashApiFactory.SetPort(int.Parse(port));
        _config.OnNext(rawConfig);
        _runningState.OnNext(Cli.RunningState.Started);
    }

    private void MergeManagedFields(RawConfig rawConfig)
    {
        void CheckAndMerge<T>(ManagedConfigValue<T> managedConfigValue, Action<RawConfig, T> action)
        {
            if (!managedConfigValue.Enabled || managedConfigValue.Value == null) return;
            action(rawConfig, managedConfigValue.Value);
        }

        var fields = _appSettings.ManagedFields;
        CheckAndMerge(fields.ExternalControllerPort, (config, v) => config.ExternalController = $"127.0.0.1:{v}");
    }

    protected void CliLogProcessor(string log)
    {
        if (string.IsNullOrEmpty(log)) return;
        var match = _logRegex.Match(log);
        if (!match.Success) match = _logMetaRegex.Match(log);
        _consoleLog.OnNext(match.Success
            ? new LogEntry(_levelsMap[match.Groups["level"].Value], match.Groups["payload"].Value)
            : new LogEntry(LogLevel.INFO, log));
    }

    protected abstract Task DoStart(string configPath);

    public async Task Stop()
    {
        _runningState.OnNext(Cli.RunningState.Stopping);
        await DoStop();
        _runningState.OnNext(Cli.RunningState.Stopped);
    }

    protected abstract Task DoStop();

    private async Task<string> EnsureConfig()
    {
        Directory.CreateDirectory(GlobalConfigs.ProgramHome);
        var filename = _profilesService.GetActiveProfile();
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new Exception("No selected profile");
        }

        var fulPath = Path.Combine(GlobalConfigs.ProfilesDir, filename);
        if (!File.Exists(fulPath))
        {
            throw new Exception("Selected profile file not exist");
        }

        return fulPath;
    }
}