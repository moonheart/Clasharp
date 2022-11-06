using ClashGui.Common.ApiModels;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public interface IRealtimeTrafficService : IObservalbeObjService<TrafficEntry>, IAutoFreshable
{
}