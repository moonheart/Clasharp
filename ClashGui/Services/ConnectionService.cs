using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Clash.Models.Connections;
using ClashGui.Cli;
using ClashGui.Services.Base;
using DynamicData;

namespace ClashGui.Services;

public class ConnectionService : BaseService<ConnectionInfo>, IConnectionService
{
    private ConnectionInfo? _prevConn;
    private readonly SourceCache<Connection, string> _items;

    public ConnectionService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashApiFactory, clashCli)
    {
        _items = new SourceCache<Connection, string>(d => d.Id);
        var observable = GetObservable();
        observable.Subscribe(d => _items.AddOrUpdate(d.Connections!));
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