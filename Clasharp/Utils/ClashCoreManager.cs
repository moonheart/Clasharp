using System.Threading.Tasks;

namespace Clasharp.Utils;

public class ClashCoreManager
{
    public async Task DownloadCore(string url)
    {
        var stream = await HttpClientHolder.Normal.GetStreamAsync(url);
        
    }
}