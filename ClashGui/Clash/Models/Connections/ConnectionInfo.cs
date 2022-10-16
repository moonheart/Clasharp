using System.Collections.Generic;

namespace ClashGui.Clash.Models.Connections;

public class ConnectionInfo
{
    public int DownloadTotal { get; set; }


    public int UploadTotal { get; set; }


    public List<Connection> Connections { get; set; }
}