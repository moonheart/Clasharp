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
    private readonly Connection _connection;
    public Connection Connection => _connection;

    public ConnectionExt(Connection connection)
    {
        _connection = connection;
    }

    public string Id => _connection.Id;
    public Metadata Metadata => _connection.Metadata;

    public string Host =>
        $"{(string.IsNullOrEmpty(Metadata.Host) ? Metadata.DestinationIP : Metadata.Host)}:{Metadata.DestinationPort}";

    public string Network => Metadata.Network;
    public string Type => Metadata.Type;
    public string Chains => string.Join("=>", _connection.Chains);
    public string Process => Path.GetFileName(Metadata.ProcessPath);
    public DateTime Start => _connection.Start;

    public string DownloadSpeed { get; private set; }
    public string DownloadTotal { get; private set; }

    public long Download
    {
        get => _connection.Download;
        set
        {
            DownloadSpeed = $"↓ {(value - _connection.Download).ToHumanSize()}/s";
            _connection.Download = value;
            DownloadTotal = $"↓ {_connection.Download.ToHumanSize()}";
        }
    }

    public string UploadSpeed { get; private set; }
    public string UploadTotal { get; private set; }

    public long Upload
    {
        get => _connection.Upload;
        set
        {
            UploadSpeed = $"↑ {(value - _connection.Upload).ToHumanSize()}/s";
            _connection.Upload = value;
            UploadTotal = $"↓ {_connection.Upload.ToHumanSize()}";
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is ConnectionExt other)
        {
            return Equals(_connection.Id, other._connection.Id);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _connection.Id.GetHashCode();
    }
}