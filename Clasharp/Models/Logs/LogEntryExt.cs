using System;
using Avalonia.Media;
using Clasharp.Clash.Models.Logs;

namespace Clasharp.Models.Logs;

public class LogEntryExt
{
    private readonly LogEntry _logEntry;

    public LogEntryExt(LogEntry logEntry)
    {
        _logEntry = logEntry;
    }


    public LogLevel Type => _logEntry.Type;
    public string Payload => _logEntry.Payload;
    
    public IBrush Brush
    {
        get
        {
            return _logEntry.Type switch
            {
                LogLevel.DEBUG => new SolidColorBrush(Colors.Gray),
                LogLevel.INFO => new SolidColorBrush(Colors.Green),
                LogLevel.WARNING => new SolidColorBrush(Colors.Orange),
                LogLevel.ERROR => new SolidColorBrush(Colors.Red),
                LogLevel.SILENT => new SolidColorBrush(Colors.OrangeRed),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public DateTime DateTime { get; set; } = DateTime.Now;
}