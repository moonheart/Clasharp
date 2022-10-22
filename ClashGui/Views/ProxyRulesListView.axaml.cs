using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.ViewModels;

namespace ClashGui.Views;

public partial class ProxyRulesListView : ReactiveUserControl<ProxyRulesListViewModel>
{
    public ProxyRulesListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}