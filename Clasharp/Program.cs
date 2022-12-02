using Avalonia;
using Avalonia.ReactiveUI;
using System;
using Avalonia.Logging;
using Serilog;

namespace Clasharp
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.log", Serilog.Events.LogEventLevel.Debug,
                    flushToDiskInterval: TimeSpan.FromSeconds(1), fileSizeLimitBytes: 1024 * 1024 * 10)
                .CreateLogger();
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace(LogEventLevel.Debug)
                .UseReactiveUI();
        }
    }
}