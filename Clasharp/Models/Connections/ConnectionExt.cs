using System;
using System.IO;
using Avalonia.Metadata;
using Clasharp.Clash.Models.Connections;
using Clasharp.ViewModels;
using Clasharp.Utils;
using ReactiveUI;

namespace Clasharp.Models.Connections;

public class ConnectionExt : ViewModelBase
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

    public string DownloadSpeedDesc { get; private set; }
    public long DownloadSpeed { get; private set; }
    public string DownloadTotal { get; private set; }

    public long Download
    {
        get => _connection.Download;
        set
        {
            if (value == _connection.Download && DownloadSpeed == 0)
                return;
            DownloadSpeed = value - _connection.Download;
            DownloadSpeedDesc = $"↓ {DownloadSpeed.ToHumanSize()}/s";
            _connection.Download = value;
            DownloadTotal = $"↓ {_connection.Download.ToHumanSize()}";
            this.RaisePropertyChanged(nameof(DownloadSpeedDesc));
            this.RaisePropertyChanged(nameof(DownloadSpeed));
            this.RaisePropertyChanged(nameof(DownloadTotal));
        }
    }

    public string UploadSpeedDesc { get; private set; }
    public long UploadSpeed { get; private set; }
    public string UploadTotal { get; private set; }

    public long Upload
    {
        get => _connection.Upload;
        set
        {
            if (value == _connection.Upload && UploadSpeed == 0)
                return;
            UploadSpeed = value - _connection.Upload;
            UploadSpeedDesc = $"↑ {UploadSpeed.ToHumanSize()}/s";
            _connection.Upload = value;
            UploadTotal = $"↓ {_connection.Upload.ToHumanSize()}";
            this.RaisePropertyChanged(nameof(UploadSpeedDesc));
            this.RaisePropertyChanged(nameof(UploadSpeed));
            this.RaisePropertyChanged(nameof(UploadTotal));
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is ConnectionExt other)
        {
            return _connection.Id == other._connection.Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _connection.Id.GetHashCode();
    }
}