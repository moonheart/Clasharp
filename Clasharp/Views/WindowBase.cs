using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.Views;

public class WindowBase<T>: ReactiveWindow<T> where T: ViewModelBase
{
    public WindowBase()
    {
        this.WhenActivated(d =>
        {
            d(ViewModel?.ShowError.RegisterHandler(ExceptionHandler.Handler)!);
        });
    }
}