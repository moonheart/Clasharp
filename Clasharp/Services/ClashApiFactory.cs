using System;
using System.Net.Http;
using Clasharp.Clash;
using Refit;
using Serilog;

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
        var refitSettings = new RefitSettings();
        refitSettings.ExceptionFactory = async message =>
        {
            var exception = await new DefaultApiExceptionFactory(refitSettings).CreateAsync(message);
            if (exception != null)
            {
                Log.Error(exception, "Failed to execute Api request");
            }

            return exception;
        };
        _api = RestService.For<IClashControllerApi>(new HttpClient()
        {
            BaseAddress = new Uri($"http://localhost:{port}"),
            Timeout = TimeSpan.FromSeconds(1)
        }, refitSettings);
    }
}