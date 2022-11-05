using System.Threading.Tasks;
using ClashGui.Clash.Models;
using ClashGui.Cli;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class VersionService : BaseService<VersionInfo>, IVersionService
{
    public VersionService(IClashCli clashCli) : base(clashCli)
    {
    }

    protected override async Task<VersionInfo> GetObj()
    {
        return await GlobalConfigs.ClashControllerApi.GetClashVersion() ?? new VersionInfo();
    }
}