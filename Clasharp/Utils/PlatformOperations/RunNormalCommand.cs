using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class RunNormalCommand : PlatformSpecificOperation<string, CommandResult>
{
    /// <summary>
    /// Execute normal command
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
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c {command}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        process.Start();
        await process.WaitForExitAsync();
        var output = await process.StandardOutput.ReadToEndAsync();
        return new CommandResult(process.ExitCode, output);
    }

    protected override async Task<CommandResult> DoForLinux(string command)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "sh",
                Arguments = $"-c '{command}'",
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