# ASP.NET WebApi + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/2e8bdc01-836e-4280-935a-e3a140c14b8c/kisslog-aspnet-webapi

**ValuesController.cs**

```csharp
using KissLog;

namespace KissLog_AspNet_WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILogger _logger;
        public ValuesController()
        {
            _logger = Logger.Factory.Get();
        }

        public IEnumerable<string> Get()
        {
            _logger.Info("Hello world from KissLog!");
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

![kisslog.net](/src/KissLog-AspNet-WebApi/KissLog-AspNet-WebApi/Content/KissLog-AspNet-WebApi.png)

DisplayName:

ASP.NET WebApi + KissLog

Description:

ASP.NET WebApi application using KissLog to save logs to kisslog.net