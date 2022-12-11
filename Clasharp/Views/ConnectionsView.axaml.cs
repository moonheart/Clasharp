using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Clasharp.ViewModels;
using Clasharp.Models.Connections;

namespace Clasharp.Views;

public partial class ConnectionsView : UserControlBase<ConnectionsViewModel>
{
    public ConnectionsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
}