using System.Diagnostics;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class RunEvaluatedCommand : PlatformSpecificOperation<string, string, RunEvaluatedCommand.Result>
{
    /// <summary>
    /// Execute evaluated command
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public override Task<Result> Exec(string filename, string arguments)
    {
        return base.Exec(filename, arguments);
    }

    protected override async Task<Result> DoForWindows(string filename, string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = true,
                Verb = "runas"
            }
        };
        process.Start();
        await process.WaitForExitAsync();
        var output = await process.StandardOutput.ReadToEndAsync();
        return new Result(process.ExitCode, output);
    }

    protected override async Task<Result> DoForLinux(string filename, string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "pkexec",
                ArgumentList = { "sh", "-c", $"{filename} {arguments}" },
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
        return new Result(process.ExitCode, error);
    }

    public class Result
    {
        public Result(int exitCode, string stdOut = "")
        {
            ExitCode = exitCode;
            StdOut = stdOut;
        }

        public int ExitCode { get; }
        public string StdOut { get; }
    }
}