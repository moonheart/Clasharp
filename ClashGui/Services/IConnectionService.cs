using System.Threading.Tasks;
using ClashGui.Clash.Models.Connections;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IConnectionService :
    IObservableListService<Connection, string>,
    IObservalbeObjService<ConnectionInfo>,
    IAutoFreshable
{
    Task CloseConnection(string id);
    Task CloseAllConnections();
}