using System.Diagnostics;
using System.Text;

namespace ClashGui.Common;

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
        _process = new Process()
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
        
        _process.OutputDataReceived += (_, args) =>
        {
            if (string.IsNullOrEmpty(args.Data)) return;
            OnNewLog?.Invoke(args.Data);
        };
        
        _process.Start();
        _process.PriorityClass = ProcessPriorityClass.High;
        _process.BeginOutputReadLine();
        
        if (_process.WaitForExit(500))
        {
            var readToEnd = _process.StandardError.ReadToEnd();
            throw new Exception(readToEnd);
        }
    }

    public void Stop()
    {
        _process?.Kill(true);
    }
}