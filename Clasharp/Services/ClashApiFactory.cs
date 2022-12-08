using System;
using System.Net.Http;
using System.Threading.Tasks;
using Clasharp.Clash;
using Refit;

namespace Clasharp.Services;

public interface IClashApiFactory
{
    IClashControllerApi Get();

    void SetPort(int port);
}

public class ClashApiFactory : IClashApiFactory
{
    private IClashControllerApi? _api;

    public IClashControllerApi Get()
    {
        if (_api == null)
        {
            throw new Exception("Port not set");
        }

        return _api;
    }

    public void SetPort(int port)
    {
        _api = RestService.For<IClashControllerApi>(new HttpClient()
        {
            BaseAddress = new Uri($"http://localhost:{port}"),
            Timeout = TimeSpan.FromSeconds(1)
        }, new RefitSettings()
        {
            ExceptionFactory = message =>
            {
                return Task.FromResult<Exception?>(null);
            },
        });
    }
}