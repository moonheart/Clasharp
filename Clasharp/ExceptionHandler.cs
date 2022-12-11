using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MessageBox.Avalonia;
using ReactiveUI;
using Serilog;

namespace Clasharp;

public static class ExceptionHandler
{
    public static async Task Handler(InteractionContext<(Exception, bool exit), Unit> arg)
    {
        Log.Error(arg.Input.Item1, "Exception from InteractionContext");
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await MessageBoxManager.GetMessageBoxStandardWindow("Error", arg.Input.Item1.Message).ShowDialog(desktop.MainWindow);
            if (arg.Input.exit)
            {
                desktop.Shutdown(1);
            }
        }
        arg.SetOutput(Unit.Default);
    }

    public static void Handler(object? o, UnobservedTaskExceptionEventArgs eventArgs)
    {
        Log.Error(eventArgs.Exception, "Exception from TaskScheduler.UnobservedTaskException");
    }
    
    public static IObserver<Exception> RxHandler = Observer.Create<Exception>(e =>
    {
        Log.Error(e, "Exception from RxApp.DefaultExceptionHandler");
    });
}