using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.ViewModels;

namespace ClashGui.Controls;

public partial class ConnectionsControl : ReactiveUserControl<ConnectionsViewModel>, IDisposable
{
    private Timer _loadRulesTimer;
    public ConnectionsControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _loadRulesTimer = new Timer(_ => LoadRules().ConfigureAwait(false).GetAwaiter().GetResult(),
            null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }
    
    private async Task LoadRules()
    {
        var connectionInfo = await GlobalConfigs.ClashControllerApi.GetConnections();
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (ViewModel != null)
            {
                ViewModel.Connections.Clear();
                foreach (var connection in connectionInfo.Connections)
                {
                    ViewModel.Connections.Add(connection);
                }

                ViewModel.DownloadTotal = connectionInfo.DownloadTotal;
                ViewModel.UploadTotal = connectionInfo.UploadTotal;
            }
        }, DispatcherPriority.Background);
    }
    
    public void Dispose()
    {
        _loadRulesTimer.Dispose();
    }
}