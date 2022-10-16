using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Models.Connections;
using ClashGui.ViewModels;

namespace ClashGui.Controls;

public partial class ConnectionsControl : ReactiveUserControl<ConnectionsViewModel>, IDisposable
{
    private Timer _loadRulesTimer;
    private DataGrid _dataGrid;
    private DataGridColumn? _sortingColumn;
    private Dictionary<DataGridColumn, ListSortDirection?> _columnSortingState = new();
    public ConnectionsControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _dataGrid = this.FindControl<DataGrid>("ConnectionsGrid");
        _dataGrid.Sorting += (_, args) =>
        {
            // if (_columnSortingState.ContainsKey(args.Column))
            // {
            //     _columnSortingState[args.Column] = _columnSortingState[args.Column] == ListSortDirection.Ascending
            //         ? ListSortDirection.Descending
            //         : ListSortDirection.Ascending;
            // }
            // else
            // {
            //     _columnSortingState[args.Column] = ListSortDirection.Ascending;
            // }

            _sortingColumn = args.Column;
        };
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
                var newConns = new List<ConnectionExt>();
                var dict = ViewModel.Connections.ToDictionary(d => d.Connection.Id, d => d);
                foreach (var connection in connectionInfo.Connections)
                {
                    if (dict.TryGetValue(connection.Id, out var conn))
                    {
                        conn.Download = connection.Download;
                        conn.Upload = connection.Upload;
                        // hashSet.Remove(connection.Id);
                        newConns.Add(conn);
                    }
                    else
                    {
                        conn = new ConnectionExt {Connection = connection};
                        newConns.Add(conn);
                    }
                }
    
                ViewModel.Connections.Clear();
                foreach (var connectionExt in newConns)
                {
                    ViewModel.Connections.Add(connectionExt);
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