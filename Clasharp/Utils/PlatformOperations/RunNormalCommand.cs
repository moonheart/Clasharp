using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class RunNormalCommand : PlatformSpecificOperation<string, string, CommandResult>
{
    /// <summary>
    /// Execute evaluated command
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public override async Task<CommandResult> Exec(string filename, string arguments)
    {
        try
        {
            return await base.Exec(filename, arguments);
        }
        catch (Exception e)
        {
            return new CommandResult(-1, e.Message);
        }
    }

    protected override async Task<CommandResult> DoForWindows(string filename, string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
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

    protected override async Task<CommandResult> DoForLinux(string filename, string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
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