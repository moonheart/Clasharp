using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using Clasharp.Models.Settings;
using Clasharp.ViewModels;
using ReactiveUI;
using Splat;

namespace Clasharp.Views;

public partial class SettingsView : UserControlBase<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            d(ViewModel?.OpenManageCoreWindow.RegisterHandler(Handler)!);
        });
        ThemeComboBox.Items = new List<FluentThemeMode>{FluentThemeMode.Dark, FluentThemeMode.Light};
    }

    private async Task Handler(InteractionContext<Unit, Unit> arg)
    {
        var profileEditWindow = new ClashCoreManageWindow {DataContext = new ClashCoreManageViewModel(Locator.Current.GetService<AppSettings>())};
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