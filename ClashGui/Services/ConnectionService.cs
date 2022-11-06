using System.Collections.Generic;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Connections;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class ConnectionService : BaseService<ConnectionInfo>, IConnectionService
{
    public ConnectionService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<ConnectionInfo> GetObj()
    {
        return await _clashApiFactory.Get().GetConnections() ?? new ConnectionInfo {Connections = new List<Connection>()};
    }

    protected override bool ObjEquals(ConnectionInfo oldObj, ConnectionInfo newObj)
    {
        return false;
    }

    public Task CloseConnection(string id)
    {
        return _clashApiFactory.Get().CloseConnection(id);
    }

    public Task CloseAllConnections()
    {
        return _clashApiFactory.Get().CloseAllConnections();
    }
}