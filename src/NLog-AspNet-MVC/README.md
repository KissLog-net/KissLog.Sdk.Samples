# ASP.NET MVC + NLog ---> kisslog.net

https://kisslog.net/RequestLogs/85c8a25f-c5b9-4069-b219-09cdcb401f28/nlog-aspnet-mvc

**HomeController.cs**

```csharp
using NLog;

namespace NLog_AspNet_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public ActionResult Index()
        {
            _logger.Info("Hello world from NLog!");
            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Fatal("Fatal message");

            return View();
        }
    }
}
```

**kisslog.net**

![kisslog.net](/src/NLog-AspNet-MVC/NLog-AspNet-MVC/Content/NLog-AspNet-MVC.png)

Description:

ASP.NET MVC application using NLog to save logs to kisslog.net