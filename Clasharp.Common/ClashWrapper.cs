using System.Diagnostics;
using System.Text;

namespace Clasharp.Common;

public class ClashWrapper
{
    private ClashLaunchInfo _clashLaunchInfo;
    private Process? _process;
    
    public Action<string>? OnNewLog { get; set; }

    public ClashWrapper(ClashLaunchInfo clashLaunchInfo)
    {
        _clashLaunchInfo = clashLaunchInfo;
    }

    public void Start()
    {
        var tmp = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _clashLaunchInfo.ExecutablePath,
                Arguments = $"-f {_clashLaunchInfo.ConfigPath} -d {_clashLaunchInfo.WorkDir}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = _clashLaunchInfo.WorkDir,
                CreateNoWindow = true,
                UseShellExecute = false,
                StandardOutputEncoding = Encoding.UTF8
            }
        };
        
        tmp.OutputDataReceived += (_, args) =>
        {
            if (string.IsNullOrEmpty(args.Data)) return;
            OnNewLog?.Invoke(args.Data);
        };
        
        tmp.Start();
        _process?.Dispose();
        _process = tmp;
        _process.PriorityClass = ProcessPriorityClass.High;
        _process.BeginOutputReadLine();
        
        if (_process.WaitForExit(500))
        {
            var readToEnd = _process.StandardError.ReadToEnd();
            throw new Exception(readToEnd);
        }
    }
    public void Test()
    {
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _clashLaunchInfo.ExecutablePath,
                Arguments = $"-f {_clashLaunchInfo.ConfigPath} -d {_clashLaunchInfo.WorkDir} -t",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = _clashLaunchInfo.WorkDir,
                CreateNoWindow = true,
                UseShellExecute = false,
                StandardOutputEncoding = Encoding.UTF8
            }
        };
        process.Start();
        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            var readToEnd = process.StandardOutput.ReadToEnd();
            throw new Exception(readToEnd);
        }
    }

    public bool IsRunning()
    {
        return !_process?.HasExited ?? false;
    }
    public void Stop()
    {
        _process?.Kill(true);
    }
}