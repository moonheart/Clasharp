using System.Threading.Tasks;
using ClashGui.Common.ApiModels.Connections;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IConnectionService: IObservalbeObjService<ConnectionInfo>, IAutoFreshable
{
    Task CloseConnection(string id);
    Task CloseAllConnections();
}