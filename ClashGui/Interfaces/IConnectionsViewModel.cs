using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;
using ClashGui.Models.Connections;

namespace ClashGui.Interfaces;

public interface IConnectionsViewModel
{
    public string DownloadTotal { get; }
    public string UploadTotal { get; }

    public string DownloadSpeed { get; }
    public string UploadSpeed { get; }
    public ReadOnlyObservableCollection<ConnectionExt> Connections { get; }

    public ConnectionExt? SelectedItem { get; set; }
}