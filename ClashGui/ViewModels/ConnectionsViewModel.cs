using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Interfaces;
using ClashGui.Models.Connections;
using ClashGui.Services;
using ClashGui.Utils;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ConnectionsViewModel : ViewModelBase, IConnectionsViewModel
{
    public override string Name => "Connections";

    public ConnectionsViewModel(IConnectionService connectionService)
    {
        _downloadTotal = connectionService.Obj.Select(d => $"↓ {d.DownloadTotal.ToHumanSize()}")
            .ToProperty(this, d => d.DownloadTotal);
        _uploadTotal = connectionService.Obj.Select(d => $"↑ {d.UploadTotal.ToHumanSize()}")
            .ToProperty(this, d => d.UploadTotal);

        connectionService.Obj.Subscribe(d =>
        {
            var previousKeys = _connectionsSource.Keys.ToHashSet();
            foreach (var connection in d.Connections)
            {
                var optional = _connectionsSource.Lookup(connection.Id);
                if (optional.HasValue)
                {
                    optional.Value.Download = connection.Download;
                    optional.Value.Upload = connection.Upload;
                }
                else
                {
                    optional = new ConnectionExt(connection);
                }

                _connectionsSource.AddOrUpdate(optional.Value);
                previousKeys.Remove(connection.Id);
            }

            foreach (var previousKey in previousKeys)
            {
                _connectionsSource.Remove(previousKey);
            }
        });

        _connectionsSource.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _connections)
            .Subscribe();

        CloseConnection = ReactiveCommand.CreateFromTask<string>(async id => 
            await GlobalConfigs.ClashControllerApi.CloeseConnection(id));
        CloseAllConnection = ReactiveCommand.CreateFromTask(async () =>
            await GlobalConfigs.ClashControllerApi.CloseAllConnections());
    }

    private readonly ObservableAsPropertyHelper<string> _downloadTotal;
    public string DownloadTotal => _downloadTotal.Value;

    private readonly ObservableAsPropertyHelper<string> _uploadTotal;
    public string UploadTotal => _uploadTotal.Value;
    public string DownloadSpeed { get; }
    public string UploadSpeed { get; }

    [Reactive]
    public ConnectionExt? SelectedItem { get; set; }

    public ReactiveCommand<string, Unit> CloseConnection { get; }
    public ReactiveCommand<Unit, Unit> CloseAllConnection { get; }

    private readonly SourceCache<ConnectionExt, string> _connectionsSource = new(d => d.Id);

    private readonly ReadOnlyObservableCollection<ConnectionExt> _connections;
    public ReadOnlyObservableCollection<ConnectionExt> Connections => _connections;
}