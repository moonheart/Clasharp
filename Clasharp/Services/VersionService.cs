using System.Threading.Tasks;
using Clasharp.Clash.Models;
using Clasharp.Cli;
using Clasharp.Services.Base;

namespace Clasharp.Services;

public class VersionService : BaseObjService<VersionInfo>, IVersionService
{
    public VersionService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<VersionInfo> GetObj()
    {
        return await _clashApiFactory.Get().GetClashVersion() ?? new VersionInfo();
    }
}