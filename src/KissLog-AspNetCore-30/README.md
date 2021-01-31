# .NET Core 3.x + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/78c66052-841c-444e-840e-7bfcf4fd99dd/kisslog-aspnetcore-3x

**HomeController.cs**

```csharp
using Microsoft.Extensions.Logging;

namespace KissLog_AspNetCore_30.Controllers
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

![kisslog.net](/src/KissLog-AspNetCore-30/KissLog-AspNetCore-30/wwwroot/KissLog-AspNetCore-30.png)

DisplayName:

.NET Core 3.x + KissLog

Description:

.NET Core 3.x application using KissLog to save logs to kisslog.net
