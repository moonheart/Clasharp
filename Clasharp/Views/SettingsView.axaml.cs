using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.Views;

public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel?.OpenManageCoreWindow.RegisterHandler(Handler)!));
    }

    private async Task Handler(InteractionContext<Unit, Unit> arg)
    {
        var profileEditWindow = new ClashCoreManageWindow {DataContext = new ClashCoreManageViewModel()};
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var res = await profileEditWindow.ShowDialog<Unit>(desktop.MainWindow);
            arg.SetOutput(res);
        }
        else
        {
            arg.SetOutput(Unit.Default);
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}