using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;
using ClashGui.Clash.Models.Connections;
using ClashGui.Models.Connections;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ConnectionsViewModel : ViewModelBase
{
    private long _downloadTotal;
    private long _uploadTotal;

    public long DownloadTotal
    {
        get => _downloadTotal;
        set => this.RaiseAndSetIfChanged(ref _downloadTotal, value);
    }


    public long UploadTotal
    {
        get => _uploadTotal;
        set => this.RaiseAndSetIfChanged(ref _uploadTotal, value);
    }


    // public AvaloniaDictionary<string, ConnectionExt> Connections { get; set; } = new();
    public AvaloniaList<ConnectionExt> Connections { get; set; } = new();
}