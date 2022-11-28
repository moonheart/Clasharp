using System.Threading.Tasks;
using Clasharp.Clash.Models.Connections;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public interface IConnectionService :
    IObservableListService<Connection, string>,
    IObservalbeObjService<ConnectionInfo>,
    IAutoFreshable
{
    Task CloseConnection(string id);
    Task CloseAllConnections();
}