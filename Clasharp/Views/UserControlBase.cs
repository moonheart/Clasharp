using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Clasharp.Models.Profiles;
using Clasharp.ViewModels;
using MessageBox.Avalonia;
using ReactiveUI;

namespace Clasharp.Views;

public class UserControlBase<T>: ReactiveUserControl<T> where T : ViewModelBase
{
    public UserControlBase()
    {
        this.WhenActivated(d =>
        {
            d(ViewModel?.ShowError.RegisterHandler(ExceptionHandler.Handler)!);
        });
    }
}