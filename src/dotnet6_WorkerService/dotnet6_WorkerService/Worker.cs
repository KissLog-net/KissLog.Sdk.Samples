using dotnet6_WorkerService.Services;

namespace dotnet6_WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IFooService _fooService;
        private readonly ILogger<Worker> _logger;
        public Worker(
            IFooService fooService,
            ILogger<Worker> logger)
        {
            _logger = logger;
            _fooService = fooService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int loopIndex = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                KissLog.Logger.SetFactory(new KissLog.LoggerFactory(new KissLog.Logger(url: $"WorkerService/{loopIndex++}")));

                _logger.LogTrace("Trace message");
                _logger.LogDebug("Debug message");
                _logger.LogInformation("Info message");
                _logger.LogWarning("Warning message");
                _logger.LogError("Error message");
                _logger.LogCritical("Critical message");
                _logger.LogError(new NullReferenceException(), "An exception");

                _fooService.Foo();

                KissLog.Logger.NotifyListeners(KissLog.Logger.Factory.Get());

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}