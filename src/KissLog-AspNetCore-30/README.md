# .NET Core 3.x + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/78c66052-841c-444e-840e-7bfcf4fd99dd/kisslog-aspnetcore-3x

**HomeController.cs**

```csharp
using KissLog;

namespace KissLog_AspNetCore_30.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Info("Hello world from KissLog!");
            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Critical("Critical message");

            return View();
        }
    }
}
```

**kisslog.net**

![kisslog.net](/src/KissLog-AspNetCore-30/KissLog-AspNetCore-30/wwwroot/KissLog-AspNetCore-30.png)

DisplayName:

.NET Core 3.x + KissLog

Description:

.NET Core 3.x application using KissLog to save logs to kisslog.net
