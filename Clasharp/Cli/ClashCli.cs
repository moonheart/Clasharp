using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Autofac;
using Clasharp.Clash.Models.Logs;
using Clasharp.Cli.ClashConfigs;
using Clasharp.Models.Settings;
using Clasharp.Utils;
using ReactiveUI;

namespace Clasharp.Cli;

public class ClashCli : IClashCli
{
    public IObservable<RawConfig> Config => ConfigSubject;
    public IObservable<RunningState> RunningState => RunningStateSubject;
    public IObservable<LogEntry> ConsoleLog => ConsoleLogSubject;

    protected readonly ReplaySubject<RunningState> RunningStateSubject = new(1);
    protected readonly ReplaySubject<LogEntry> ConsoleLogSubject = new(1);
    protected readonly ReplaySubject<RawConfig> ConfigSubject = new(1);


    private readonly IClashCli _local;
    private readonly IClashCli _remote;

    private readonly AppSettings _appSettings;
    private RawConfig? _currentConfig;

    public ClashCli(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _local = ContainerProvider.Container.ResolveNamed<IClashCli>("local");
        _remote = ContainerProvider.Container.ResolveNamed<IClashCli>("remote");

        Sub(_local.Config, _remote.Config, ConfigSubject);
        Sub(_local.ConsoleLog, _remote.ConsoleLog, ConsoleLogSubject);
        MessageBus.Current.Listen<LogEntry>().Subscribe(ConsoleLogSubject.OnNext);
        Sub(_local.RunningState, _remote.RunningState, RunningStateSubject);

        ConfigSubject.Subscribe(d => _currentConfig = d);
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
                await ProxyUtils.UnsetSystemProxy();
                break;
            case SystemProxyMode.SetProxy when _currentConfig != null:
            {
                await ProxyUtils.SetSystemProxy("127.0.0.1",
                    _currentConfig.MixedPort ?? _currentConfig.Port ?? throw new Exception("No valid proxy port"),
                    Array.Empty<string>());
                break;
            }
        }
    }

    public async Task<RawConfig> GenerateConfig()
    {
        return _appSettings.UseServiceMode ? await _remote.GenerateConfig() : await _local.GenerateConfig();
    }

    public async Task Stop()
    {
        switch (_appSettings.SystemProxyMode)
        {
            case SystemProxyMode.SetProxy:
            {
                await ProxyUtils.UnsetSystemProxy();
                break;
            }
        }

        await (_appSettings.UseServiceMode ? _remote.Stop() : _local.Stop());
    }
}