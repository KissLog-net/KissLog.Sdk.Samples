# .NET Core 3.x + KissLog &#8680; kisslog.net


KissLog + HomeController.cs                               | kisslog.net                                                                                                 |
--------------------------------------------------------- | --------------------------------------------------------- ------------------------------------------------- |
```                                                       | ![Image of Yaktocat](/src/KissLog-AspNetCore-30/KissLog-AspNetCore-30/wwwroot/KissLog-AspNetCore-3x.png)    |
`using KissLog; `                                         |                                                                                                             |
                                                          |                                                                                                             |
`namespace KissLog_AspNetCore_30.Controllers`             |                                                                                                             |
{                                                         |                                                                                                             |
    public class HomeController : Controller              |                                                                                                             |
    {                                                     |                                                                                                             |
        private readonly ILogger _logger;                 |                                                                                                             |
        public HomeController(                            |                                                                                                             |
            ILogger logger)                               |                                                                                                             |
        {                                                 |                                                                                                             |
            _logger = logger;                             |                                                                                                             |
        }                                                 |                                                                                                             |
                                                          |                                                                                                             |
        public IActionResult Index()                      |                                                                                                             |
        {                                                 |                                                                                                             |
            ::_logger.Info("Hello world from KissLog!");  |                                                                                                             |
            _logger.Trace("Trace message");               |                                                                                                             |
            _logger.Debug("Debug message");               |                                                                                                             |
            _logger.Info("Info message");                 |                                                                                                             |
            _logger.Warn("Warning message");              |                                                                                                             |
            _logger.Error("Error message");               |                                                                                                             |
            _logger.Critical("Critical message");         |                                                                                                             |
                                                          |                                                                                                             |
            return View();                                |                                                                                                             |
        }                                                 |                                                                                                             |
    }                                                     |                                                                                                             |
}                                                         |                                                                                                             |
```                                                       |                                                                                                             |
                                                          |                                                                                                             |