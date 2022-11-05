using ClashGui.Clash.Models.Connections;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IConnectionService: IObservalbeObjService<ConnectionInfo>, IAutoFreshable
{
    
}