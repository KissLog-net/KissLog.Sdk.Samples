# ASP.NET MVC + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/35f66045-16df-4a3a-9cb4-b1762b464348/kisslog-aspnet-mvc

**HomeController.cs**

```csharp
using KissLog;

namespace KissLog_AspNet_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController()
        {
            _logger = Logger.Factory.Get();
        }

        public ActionResult Index()
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

![kisslog.net](/src/KissLog-AspNet-MVC/KissLog-AspNet-MVC/Content/KissLog-AspNet-MVC.png)

DisplayName:

ASP.NET MVC

Description:

ASP.NET MVC application using KissLog to save logs to kisslog.net