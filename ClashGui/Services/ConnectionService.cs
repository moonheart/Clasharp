using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Connections;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ConnectionService : BaseService<ConnectionInfo>, IConnectionService
{
    public ConnectionService(IClashCli clashCli) : base(clashCli)
    {
    }

    protected override async Task<ConnectionInfo> GetObj()
    {
        return await GlobalConfigs.ClashControllerApi.GetConnections() ?? new ConnectionInfo {Connections = new List<Connection>()};
    }

    protected override bool ObjEquals(ConnectionInfo oldObj, ConnectionInfo newObj)
    {
        return false;
    }
}