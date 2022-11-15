using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.Models.Profiles;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Views;

public partial class ProfilesView : ReactiveUserControl<ProfilesViewModel>
{
    public ProfilesView()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel?.EditProfile.RegisterHandler(Handler)!));
    }

    private async Task Handler(InteractionContext<Profile?, Profile?> obj)
    {
        var profileEditWindow = new ProfileEditWindow {DataContext = new ProfileEditViewModel(obj.Input)};
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var res = await profileEditWindow.ShowDialog<Profile>(desktop.MainWindow);
            obj.SetOutput(res);
        }
        else
        {
            obj.SetOutput(null);
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}