using KissLog;
using System;

namespace netframework_WindowsService.Services
{
    internal class FooService : IFooService
    {
        private readonly IKLogger _logger;
        public FooService(IKLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Foo()
        {
            _logger.Debug("Foo executed");
        }
    }
}
