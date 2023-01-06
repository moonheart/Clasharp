using System;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Logs;
using Clasharp.Cli.ClashConfigs;

namespace Clasharp.Cli;

public interface IClashCli
{
    IObservable<RawConfig> Config { get; }

    IObservable<RunningState> RunningState { get; }

    IObservable<LogEntry> ConsoleLog { get; }

    Task Start();

    Task<RawConfig> GenerateConfig();

    Task Stop();
}