using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Logging;
using ReactiveUI;
using Serilog;

namespace Clasharp
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.log", Serilog.Events.LogEventLevel.Debug,
                    flushToDiskInterval: TimeSpan.FromSeconds(1), fileSizeLimitBytes: 1024 * 1024 * 10)
                .CreateLogger();

            TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
            {
                Log.Error(eventArgs.Exception, "Exception from TaskScheduler.UnobservedTaskException");
            };
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(e =>
            {
                Log.Error(e, "Exception from RxApp.DefaultExceptionHandler");
            });
            
            try
            {
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Fatal error happened, now exit");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace(LogEventLevel.Debug)
                .UseReactiveUI();
        }
    }
}