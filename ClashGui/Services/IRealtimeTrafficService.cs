using ClashGui.Clash.Models;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IRealtimeTrafficService : IObservalbeObjService<TrafficEntry>, IAutoFreshable
{
}