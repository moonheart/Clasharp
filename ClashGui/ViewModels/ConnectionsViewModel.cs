using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;
using ClashGui.Clash.Models.Connections;
using ClashGui.Interfaces;
using ClashGui.Models.Connections;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ConnectionsViewModel : ViewModelBase, IConnectionsViewModel
{
    private long _downloadTotal;
    private long _uploadTotal;

    [Reactive]
    public long DownloadTotal { get; set; }

    [Reactive]
    public long UploadTotal { get; set; }


    // public AvaloniaDictionary<string, ConnectionExt> Connections { get; set; } = new();
    public AvaloniaList<ConnectionExt> Connections { get; set; } = new();
}