using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Logs;
using ClashGui.Common;
using ClashGui.Services;
using LogLevel = ClashGui.Clash.Models.Logs.LogLevel;

namespace ClashGui.Cli;

public class ClashLocalCli : ClashCliBase
{
    private Process? _process;

    public ClashLocalCli(IClashApiFactory clashApiFactory)
    {
        _clashApiFactory = clashApiFactory;
        _runningState.OnNext(Cli.RunningState.Stopped);
    }

    protected override async Task DoStart()
    {
        _process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = GlobalConfigs.ClashExe,
                Arguments = $"-f config.yaml -d {GlobalConfigs.ProgramHome}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = GlobalConfigs.ProgramHome,
                CreateNoWindow = true,
                UseShellExecute = false,
                StandardOutputEncoding = Encoding.UTF8,
            }
        };

        _process.OutputDataReceived += (_, args) =>
        {
            if (string.IsNullOrEmpty(args.Data)) return;
            var match = _logRegex.Match(args.Data);
            if (!match.Success) match = _logMetaRegex.Match(args.Data);
            _consoleLog.OnNext(match.Success
                ? new LogEntry(_levelsMap[match.Groups["level"].Value], match.Groups["payload"].Value)
                : new LogEntry(LogLevel.INFO, args.Data));
        };
        _process.Start();
        _process.PriorityClass = ProcessPriorityClass.High;
        _process.BeginOutputReadLine();

        if (_process.WaitForExit(500))
        {
            var readToEnd = await _process.StandardError.ReadToEndAsync();
            throw new Exception(readToEnd);
        }
    }

    protected override Task DoStop()
    {
        _process?.Kill(true);
        return Task.CompletedTask;
    }

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