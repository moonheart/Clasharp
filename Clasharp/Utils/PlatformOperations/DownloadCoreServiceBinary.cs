using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;
using Clasharp.Common;

namespace Clasharp.Utils.PlatformOperations;

public class DownloadCoreServiceBinary : PlatformSpecificOperation<string?, string>
{
    private readonly RunEvaluatedCommand _runEvaluatedCommand = new();

    /// <summary>
    /// Download Clash meta binary
    /// </summary>
    /// <param name="downloadUrl">optional download url</param>
    /// <returns>Downloaded file location</returns>
    public override Task<string> Exec(string? downloadUrl)
    {
        return base.Exec(downloadUrl);
    }

    private async Task<string> GetLatestReleases()
    {
        var releasesLatest = "https://api.github.com/repos/MetaCubeX/Clash.Meta/releases/latest";
        var json = await HttpClientHolder.Normal.GetStringAsync(releasesLatest);
        var element = JsonSerializer.Deserialize<JsonElement>(json);
        return element.GetProperty("tag_name").GetString() ??
               throw new Exception($"Failed to get release info from {releasesLatest}");
    }

    protected override async Task<string> DoForWindows(string? downloadUrl)
    {
        if (string.IsNullOrWhiteSpace(downloadUrl))
        {
            var release = await GetLatestReleases();
            downloadUrl =
                $"https://github.com/MetaCubeX/Clash.Meta/releases/download/{release}/Clash.Meta-windows-amd64-{release}.zip";
        }

        var tempPath = Path.GetTempPath();
        await using (var stream = await HttpClientHolder.Normal.GetStreamAsync(downloadUrl))
        {
            using (var zipArchive = new ZipArchive(stream))
            {
                zipArchive.ExtractToDirectory(tempPath, true);
            }
        }

        var sourceFileName = Path.Combine(tempPath, "Clash.Meta-windows-amd64.exe");
        return sourceFileName;
    }

    protected override async Task<string> DoForLinux(string? downloadUrl)
    {
        if (string.IsNullOrWhiteSpace(downloadUrl))
        {
            var release = await GetLatestReleases();
            downloadUrl =
                $"https://github.com/MetaCubeX/Clash.Meta/releases/download/{release}/Clash.Meta-linux-amd64-{release}.gz";
        }

        var tempFileName = Path.GetTempFileName();
        await using (var stream = await HttpClientHolder.Normal.GetStreamAsync(downloadUrl))
        {
            await using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
            {
                await using (var fileStream = File.OpenWrite(tempFileName))
                {
                    await gZipStream.CopyToAsync(fileStream);
                }
            }
        }

        await _runEvaluatedCommand.Exec($"chmod +x {tempFileName}");
        return tempFileName;
    }
}