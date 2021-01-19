# ASP.NET WebApi + NLog ---> kisslog.net

https://kisslog.net/RequestLogs/c91540a5-4041-42f8-99e6-f6cf6c98787c/nlog-aspnet-webapi

**ValuesController.cs**

```csharp
using NLog;

namespace NLog_AspNet_WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILogger _logger;
        public ValuesController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IEnumerable<string> Get()
        {
            _logger.Info("Hello world from NLog!");
            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Fatal("Fatal message");

            return new string[] { "value1", "value2" };
        }
    }
}
```

**kisslog.net**

![kisslog.net](/src/NLog-AspNet-WebApi/NLog-AspNet-WebApi/Content/NLog-AspNet-WebApi.png)

DisplayName:

ASP.NET WebApi + NLog

Description:

ASP.NET WebApi application using NLog to save logs to kisslog.net