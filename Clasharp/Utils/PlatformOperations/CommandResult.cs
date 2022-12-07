namespace Clasharp.Utils.PlatformOperations;

public class CommandResult
{
    public CommandResult(int exitCode, string stdOut = "")
    {
        ExitCode = exitCode;
        StdOut = stdOut;
    }

    public int ExitCode { get; }
    public string StdOut { get; }
}