using System;
using System.IO;
using Avalonia.Metadata;
using ClashGui.Clash.Models.Connections;
using ClashGui.Utils;
using ClashGui.ViewModels;
using ReactiveUI;

namespace ClashGui.Models.Connections;

public class ConnectionExt
{
    public Connection Connection { get; set; }

    public ConnectionExt()
    {
    }

    public Metadata Metadata => Connection.Metadata;

    public string Host =>
        $"{(string.IsNullOrEmpty(Metadata.Host) ? Metadata.DestinationIP : Metadata.Host)}:{Metadata.DestinationPort}";

    public string Network => Metadata.Network;
    public string Type => Metadata.Type;
    public string Chains => string.Join("=>", Connection.Chains);
    public string Process => Path.GetFileName(Metadata.ProcessPath);
    public DateTime Start => Connection.Start;

    public string DownloadSpeed { get; private set; }

    public long Download
    {
        get => Connection.Download;
        set
        {
            DownloadSpeed = $"↓ {(value - Connection.Download).ToHumanSize()}";
            Connection.Download = value;
        }
    }

    public string UploadSpeed { get; private set; }

    public long Upload
    {
        get => Connection.Upload;
        set
        {
            UploadSpeed = $"↑ {(value - Connection.Upload).ToHumanSize()}";
            Connection.Upload = value;
        }
    }

    // public string Speed => $"↓ {DownloadSpeed.ToHumanSize()} ↑ {UploadSpeed.ToHumanSize()}";
}