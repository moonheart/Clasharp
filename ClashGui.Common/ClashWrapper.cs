using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using ClashGui.Common.ApiModels.Logs;

namespace ClashGui.Common;

public class ClashWrapper
{
    private ClashLaunchInfo _clashLaunchInfo;
    private Process? _process;
    
    public Action<LogEntry>? OnNewLog { get; set; }

    private Regex _logRegex = new Regex(@"\d{2}\:\d{2}\:\d{2}\s+(?<level>\S+)\s*(?<module>\[.+?\])?\s*(?<payload>.+)");
    private Regex _logMetaRegex = new Regex(@"time=""(.+?) level=(?<level>.+?) msg=""(?<payload>.+)""");

    private Dictionary<string, LogLevel> _levelsMap = new()
    {
        ["DBG"] = LogLevel.DEBUG,
        ["INF"] = LogLevel.INFO,
        ["WRN"] = LogLevel.WARNING,
        ["ERR"] = LogLevel.ERROR,
        ["SLT"] = LogLevel.SILENT,
        ["debug"] = LogLevel.DEBUG,
        ["info"] = LogLevel.INFO,
        ["warning"] = LogLevel.WARNING,
        ["error"] = LogLevel.ERROR,
        ["silent"] = LogLevel.SILENT,
    };
    
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
                Arguments = $"-f config.yaml -d {_clashLaunchInfo.WorkDir}",
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
            var match = _logRegex.Match(args.Data);
            if (!match.Success) match = _logMetaRegex.Match(args.Data);
            OnNewLog?.Invoke(match.Success
                ? new LogEntry(_levelsMap[match.Groups["level"].Value], match.Groups["payload"].Value)
                : new LogEntry(LogLevel.INFO, args.Data));
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
}