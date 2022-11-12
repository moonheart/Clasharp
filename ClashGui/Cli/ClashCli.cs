using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Autofac;
using ClashGui.Clash.Models.Logs;
using ClashGui.Cli.ClashConfigs;
using ClashGui.Models.Settings;
using ClashGui.Utils;

namespace ClashGui.Cli;

public class ClashCli : IClashCli
{
    public IObservable<RawConfig> Config => _config;
    public IObservable<RunningState> RunningState => _runningState;
    public IObservable<LogEntry> ConsoleLog => _consoleLog;

    protected ReplaySubject<RunningState> _runningState = new(1);
    protected ReplaySubject<LogEntry> _consoleLog = new(1);
    protected ReplaySubject<RawConfig> _config = new(1);


    private IClashCli _local;
    private IClashCli _remote;

    private AppSettings _appSettings;
    private RawConfig? _currentConfig;

    public ClashCli(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _local = ContainerProvider.Container.ResolveNamed<IClashCli>("local");
        _remote = ContainerProvider.Container.ResolveNamed<IClashCli>("remote");

        Sub(_local.Config, _remote.Config, _config);
        Sub(_local.ConsoleLog, _remote.ConsoleLog, _consoleLog);
        Sub(_local.RunningState, _remote.RunningState, _runningState);

        _config.Subscribe(d => _currentConfig = d);
    }

    private void Sub<T>(IObservable<T> sourceLocal, IObservable<T> sourceRemote, ReplaySubject<T> target)
    {
        sourceLocal.Where(_ => !_appSettings.UseServiceMode).Subscribe(target.OnNext);
        sourceRemote.Where(_ => _appSettings.UseServiceMode).Subscribe(target.OnNext);
    }

    public async Task Start()
    {
        if (_appSettings.UseServiceMode)
        {
            await _remote.Start();
        }
        else
        {
            await _local.Start();
        }

        switch (_appSettings.SystemProxyMode)
        {
            case SystemProxyMode.Clear:
                ProxyUtils.UnsetSystemProxy();
                break;
            case SystemProxyMode.SetProxy when _currentConfig != null:
            {
                ProxyUtils.SetSystemProxy($"http://127.0.0.1:{_currentConfig.MixedPort ?? _currentConfig.Port}", "");
                break;
            }
        }
    }

    public async Task Stop()
    {
        await (_appSettings.UseServiceMode ? _remote.Stop() : _local.Stop());
        switch (_appSettings.SystemProxyMode)
        {
            case SystemProxyMode.SetProxy:
            {
                ProxyUtils.UnsetSystemProxy();
                break;
            }
        }
    }
}