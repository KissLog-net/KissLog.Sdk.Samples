# .NET Core 2.x + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/7205e6f5-0551-43a6-a37c-80b98ddfa44e/kisslog-aspnetcore-2x

**HomeController.cs**

```csharp
using Microsoft.Extensions.Logging;

namespace KissLog_AspNetCore_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello world from KissLog!");

            _logger.LogTrace("Trace message");
            _logger.LogDebug("Debug message");
            _logger.LogInformation("Info message");
            _logger.LogWarning("Warning message");
            _logger.LogError("Error message");
            _logger.LogCritical("Critical message");

            return View();
        }
    }
}
```

**kisslog.net**

![kisslog.net](/src/KissLog-AspNetCore-20/KissLog-AspNetCore-20/wwwroot/KissLog-AspNetCore-20.png)

DisplayName:

.NET Core 2.x + KissLog

Description:

.NET Core 2.x application using KissLog to save logs to kisslog.net