using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MsBox.Avalonia;
using ReactiveUI;
using Refit;
using Serilog;

namespace Clasharp;

public static class ExceptionHandler
{
    public static async Task Handler(InteractionContext<(Exception, bool exit), Unit> arg)
    {
        Log.Error(arg.Input.Item1, "Exception from InteractionContext");
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await MessageBoxManager.GetMessageBoxStandard("Error", arg.Input.Item1.Message).ShowWindowDialogAsync(desktop.MainWindow);
            if (arg.Input.exit)
            {
                desktop.Shutdown(1);
            }
        }
        arg.SetOutput(Unit.Default);
    }

    public static void HandleException(Exception exception, bool exit = false)
    {
        Log.Error(exception, exception.Message);
        if (exit)
        {
            Environment.Exit(1);
        }
    }

    public static RefitSettings AddExceptionHandler(this RefitSettings refitSettings)
    {
        refitSettings.ExceptionFactory = async message =>
        {
            var exception = await new DefaultApiExceptionFactory(refitSettings).CreateAsync(message);
            if (exception != null)
            {
                Log.Error(exception, "Failed to execute Api request");
            }

            return exception;
        };
        return refitSettings;
    }

    public static void TaskScheduleExceptionHandler(object? o, UnobservedTaskExceptionEventArgs eventArgs)
    {
        Log.Error(eventArgs.Exception, "Exception from TaskScheduler.UnobservedTaskException");
    }
    
    public static readonly IObserver<Exception> RxHandler = Observer.Create<Exception>(e =>
    {
        Log.Error(e, "Exception from RxApp.DefaultExceptionHandler");
    });
}