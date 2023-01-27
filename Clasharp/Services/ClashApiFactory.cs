using System;
using Clasharp.Clash;
using Clasharp.Utils;
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
            throw new InvalidOperationException("Port not set");
        }

        return _api;
    }

    public void SetPort(int port)
    {
        _api = RestService.For<IClashControllerApi>(
            HttpClientHolder.For($"http://localhost:{port}"),
            new RefitSettings().AddExceptionHandler());
    }
}