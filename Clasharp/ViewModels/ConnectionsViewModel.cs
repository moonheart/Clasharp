using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Clasharp.Interfaces;
using Clasharp.Models.Connections;
using Clasharp.Services;
using Clasharp.Utils;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Clasharp.ViewModels;

public class ConnectionsViewModel : ViewModelBase, IConnectionsViewModel
{
    public override string Name => Resources.titleConnections;

    public ConnectionsViewModel(IConnectionService connectionService)
    {
        _downloadTotal = connectionService.Obj.Select(d => $"↓ {d.DownloadTotal.ToHumanSize()}")
            .ToProperty(this, d => d.DownloadTotal);
        _uploadTotal = connectionService.Obj.Select(d => $"↑ {d.UploadTotal.ToHumanSize()}")
            .ToProperty(this, d => d.UploadTotal);

        connectionService.List
            .Transform(d => new ConnectionExt(d))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _connections, adaptor: new ConnectionChangeAdaptor())
            .Subscribe();

        CloseConnection = ReactiveCommand.CreateFromTask<string>(async id =>
            await connectionService.CloseConnection(id));
        CloseAllConnection = ReactiveCommand.CreateFromTask(async () =>
            await connectionService.CloseAllConnections());
        this.WhenAnyValue(d => d.SelectedItem).Subscribe(d => { Debug.WriteLine("--------1----------" + d?.Id); });
    }

    private sealed class ConnectionChangeAdaptor : IObservableCollectionAdaptor<ConnectionExt, string>
    {
        public void Adapt(IChangeSet<ConnectionExt, string> changes, IObservableCollection<ConnectionExt> collection)
        {
            foreach (var change in changes)
            {
                switch (change.Reason)
                {
                    case ChangeReason.Add:
                        collection.Add(change.Current);
                        break;
                    case ChangeReason.Update:
                        var indexOf = collection.IndexOf(change.Current);
                        collection[indexOf].Download = change.Current.Download;
                        collection[indexOf].Upload = change.Current.Upload;
                        break;
                    case ChangeReason.Remove:
                        collection.Remove(change.Current);
                        break;
                    case ChangeReason.Refresh:
                        break;
                    case ChangeReason.Moved:
                        break;
                }
            }
        }
    }

    private readonly ObservableAsPropertyHelper<string> _downloadTotal;
    public string DownloadTotal => _downloadTotal.Value;

    private readonly ObservableAsPropertyHelper<string> _uploadTotal;
    public string UploadTotal => _uploadTotal.Value;
    public string DownloadSpeed { get; } = "";
    public string UploadSpeed { get; } = "";


    private string? _connectionId;

    public ConnectionExt? SelectedItem
    {
        get => _connections.FirstOrDefault(d => d.Id == _connectionId);
        set { this.RaiseAndSetIfChanged(ref _connectionId, value?.Id); }
    }

    public ReactiveCommand<string, Unit> CloseConnection { get; }
    public ReactiveCommand<Unit, Unit> CloseAllConnection { get; }

    private readonly MyReadOnlyObservableCollection<ConnectionExt> _connections;
    public ReadOnlyObservableCollection<ConnectionExt> Connections => _connections;
}

public sealed class MyReadOnlyObservableCollection<T> : ReadOnlyObservableCollection<T>
{
    public MyReadOnlyObservableCollection(ObservableCollection<T> list) : base(list)
    {
        CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (var eNewItem in e.NewItems)
            {
                ((INotifyPropertyChanged) eNewItem).PropertyChanged += OnItemPropertyChanged;
            }
        }

        if (e.OldItems != null)
        {
            foreach (var eOldItem in e.OldItems)
            {
                ((INotifyPropertyChanged) eOldItem).PropertyChanged -= OnItemPropertyChanged;
            }
        }
    }

    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender,
            IndexOf((T) sender));
        OnCollectionChanged(args);
    }
}