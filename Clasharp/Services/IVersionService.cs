using Clasharp.Clash.Models;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public interface IVersionService : IObservalbeObjService<VersionInfo>, IAutoFreshable
{
}