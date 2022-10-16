using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClashGui.Clash.Models.Connections;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ConnectionsViewModel : ViewModelBase
{
    private int _downloadTotal;
    private int _uploadTotal;

    public int DownloadTotal
    {
        get => _downloadTotal;
        set => this.RaiseAndSetIfChanged(ref _downloadTotal, value);
    }


    public int UploadTotal
    {
        get => _uploadTotal;
        set => this.RaiseAndSetIfChanged(ref _uploadTotal, value);
    }


    public ObservableCollection<Connection> Connections { get; set; } = new();
}