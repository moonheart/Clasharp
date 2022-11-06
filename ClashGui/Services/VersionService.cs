using System.Threading.Tasks;
using ClashGui.Cli;
using ClashGui.Common.ApiModels;
using ClashGui.Services.Base;

namespace ClashGui.Services;

public class VersionService : BaseService<VersionInfo>, IVersionService
{
    public VersionService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override async Task<VersionInfo> GetObj()
    {
        return await _clashApiFactory.Get().GetClashVersion() ?? new VersionInfo();
    }
}