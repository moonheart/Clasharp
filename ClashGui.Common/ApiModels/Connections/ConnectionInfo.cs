namespace ClashGui.Common.ApiModels.Connections;

public class ConnectionInfo
{
    public long DownloadTotal { get; set; }

    public long UploadTotal { get; set; }

    public List<Connection> Connections { get; set; }
}