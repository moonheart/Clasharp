using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;
using MessageBox.Avalonia;
using ReactiveUI;

namespace Clasharp.Views;

public class WindowBase<T>: ReactiveWindow<T> where T: ViewModelBase
{
    public WindowBase()
    {
        this.WhenActivated(d =>
        {
            d(ViewModel?.ShowError.RegisterHandler(Handler)!);
        });
    }
    
    private async Task Handler(InteractionContext<(Exception, bool exit), Unit> arg)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await MessageBoxManager.GetMessageBoxStandardWindow("Error", arg.Input.Item1.Message).ShowDialog(this);
            if (arg.Input.exit)
            {
                desktop.Shutdown(1);
            }
        }
        arg.SetOutput(Unit.Default);
    }
}