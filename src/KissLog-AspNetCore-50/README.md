# .NET Core 5.x + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/a6171034-d02f-4df7-9ba6-b6ba4becbf9d/kisslog-aspnetcore-5x

**HomeController.cs**

```csharp
using Microsoft.Extensions.Logging;

namespace KissLog_AspNetCore_50.Controllers
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

![kisslog.net](/src/KissLog-AspNetCore-50/KissLog-AspNetCore-50/wwwroot/KissLog-AspNetCore-50.png)

DisplayName:

.NET Core 5.x + KissLog

Description:

.NET Core 5.x application using KissLog to save logs to kisslog.net
