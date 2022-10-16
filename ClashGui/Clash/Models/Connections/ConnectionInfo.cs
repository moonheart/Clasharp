using System.Collections.Generic;

namespace ClashGui.Clash.Models.Connections;

public class ConnectionInfo
{
    public long DownloadTotal { get; set; }


    public long UploadTotal { get; set; }


    public List<Connection> Connections { get; set; }
}