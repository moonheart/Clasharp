using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Clasharp.ViewModels;

namespace Clasharp.Views;

public partial class ProxyRulesListView : UserControlBase<ProxyRulesListViewModel>
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