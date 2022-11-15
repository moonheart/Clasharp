using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Views;

public partial class ProfileEditWindow : ReactiveWindow<ProfileEditViewModel>
{
    public ProfileEditWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(d =>
        {
            d(ViewModel.Save.Subscribe(Close));
            Title = ViewModel.IsCreate ? "Create Profile" : "Edit Profle";
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}