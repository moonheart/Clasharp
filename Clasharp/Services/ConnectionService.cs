using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Clasharp.Clash.Models.Connections;
using Clasharp.Cli;
using Clasharp.Services.Base;
using DynamicData;

namespace Clasharp.Services;

public class ConnectionService : BaseService<ConnectionInfo>, IConnectionService
{
    private ConnectionInfo? _prevConn;
    private readonly SourceCache<Connection, string> _items;

    public ConnectionService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashApiFactory, clashCli)
    {
        _items = new SourceCache<Connection, string>(d => d.Id);
        var observable = GetObservable();
        observable.Subscribe(d => _items.EditDiff(d.Connections!, EqualityComparer<Connection>.Default));
        Obj = observable.Where(d =>
        {
            if (_prevConn != null && Equals(_prevConn, d)) return false;
            _prevConn = d;
            return true;
        });
    }

    protected override async Task<ConnectionInfo> GetObj()
    {
        return await _clashApiFactory.Get().GetConnections() ?? new ConnectionInfo {Connections = new List<Connection>()};
    }

    public Task CloseConnection(string id)
    {
        return _clashApiFactory.Get().CloseConnection(id);
    }

    public Task CloseAllConnections()
    {
        return _clashApiFactory.Get().CloseAllConnections();
    }

    public IObservable<IChangeSet<Connection, string>> List => _items.Connect();

    public IObservable<ConnectionInfo> Obj { get; }
}