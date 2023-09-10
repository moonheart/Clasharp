using System.Collections.Concurrent;
using Clasharp.Common;
using Grpc.Core;

namespace Clasharp.Service
{
    internal class CoreServiceImpl : Common.CoreService.CoreServiceBase
    {
        private readonly ILogger<CoreServiceImpl> _logger;
        private static volatile ClashWrapper? _clashWrapper;

        public CoreServiceImpl(ILogger<CoreServiceImpl> logger)
        {
            _logger = logger;
        }

        public override async Task Logs(GetRealtimeLogs request, IServerStreamWriter<LogMessage> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                if (_clashWrapper != null && _clashWrapper.LogsQueue.TryDequeue(out var log))
                {
                    await responseStream.WriteAsync(new LogMessage() { Message = log });
                }
                else
                {
                    await Task.Delay(100, context.CancellationToken);
                }
            }
        }

        public override Task<StartClashResponse> StartClash(StartClashRequest request, ServerCallContext context)
        {
            _clashWrapper?.Stop();
            _clashWrapper = new ClashWrapper(new ClashLaunchInfo()
            {
                ConfigPath = request.ConfigPath,
                ExecutablePath = request.ExecutablePath,
                WorkDir = request.WorkDir
            });
            _clashWrapper.Start();
            return Task.FromResult(new StartClashResponse());
        }

        public override Task<StopClashResponse> StopClash(StopClashRequest request, ServerCallContext context)
        {
            _clashWrapper?.Stop();
            return Task.FromResult(new StopClashResponse());
        }

        public override Task<RunningState> IsRunning(GetRunningState request, ServerCallContext context)
        {
            var running = _clashWrapper?.IsRunning() ?? false;
            return Task.FromResult(new RunningState() { IsRunning = running });
        }
    }
}