using System;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Logs;
using ClashGui.Cli.ClashConfigs;

namespace ClashGui.Cli;

public interface IClashCli
{
    IObservable<RawConfig> Config { get; }

    IObservable<RunningState> RunningState { get; }

    IObservable<LogEntry> ConsoleLog { get; }

    Task Start();
    Task Stop();
}