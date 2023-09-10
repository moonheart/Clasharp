using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class RunEvaluatedCommand : PlatformSpecificOperation<string, CommandResult>
{
    /// <summary>
    /// Execute evaluated command
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public override async Task<CommandResult> Exec(string command)
    {
        try
        {
            return await base.Exec(command);
        }
        catch (Exception e)
        {
            return new CommandResult(-1, e.Message);
        }
    }

    protected override async Task<CommandResult> DoForWindows(string command)
    {
        var currentFilename = Process.GetCurrentProcess().MainModule!.FileName;
        var gsudo = Path.Combine(Path.GetDirectoryName(currentFilename)!, "gsudo.exe");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = gsudo,
                Arguments = $"cmd /c {command}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        process.Start();
        await process.WaitForExitAsync();
        return new CommandResult(process.ExitCode, "cannot get output of evaluated process output on windows");
    }

    protected override async Task<CommandResult> DoForLinux(string command)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "pkexec",
                ArgumentList = { "bash", "-c", $"{command}" },
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        process.Start();
        await process.WaitForExitAsync();
        var error = await process.StandardError.ReadToEndAsync();
        if (string.IsNullOrWhiteSpace(error))
        {
            error = await process.StandardOutput.ReadToEndAsync();
        }
        return new CommandResult(process.ExitCode, error);
    }
}