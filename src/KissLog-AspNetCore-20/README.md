# .NET Core 2.x + KissLog &#8680; kisslog.net

**HomeController.cs**

```csharp
using KissLog;

namespace KissLog_AspNetCore_20.Controllers
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

![kisslog.net](/src/KissLog-AspNetCore-20/KissLog-AspNetCore-20/wwwroot/KissLog-AspNetCore-2x.png)