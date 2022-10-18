using Avalonia.Collections;
using ClashGui.Models.Connections;

namespace ClashGui.Interfaces;

public interface IConnectionsViewModel
{
    public long DownloadTotal { get; set; }
    public long UploadTotal { get; set; }
    public AvaloniaList<ConnectionExt> Connections { get; set; }
}