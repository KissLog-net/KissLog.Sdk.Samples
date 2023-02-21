using dotnet6_WebApp.ActionFilters;
using dotnet6_WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace dotnet6_WebApp.Controllers
{
    [TrackExecutionTime]
    public class HomeController : Controller
    {
        private readonly IFooService _fooService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        public HomeController(
            IFooService fooService,
            IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            _fooService = fooService;
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Trace message");
            _logger.LogDebug("Debug message");
            _logger.LogInformation("Info message");
            _logger.LogWarning("Warning message");
            _logger.LogError("Error message");
            _logger.LogCritical("Critical message");
            _logger.LogError(new NullReferenceException(), "An exception");

            _fooService.Foo();

            string applicationId = _configuration["KissLog.ApplicationId"];
            string logsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslogsampleapp";
            string textLogs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            var sb = new StringBuilder();
            sb.AppendLine("KissLog.net logs:");
            sb.AppendLine(logsUrl);
            sb.AppendLine();
            sb.AppendLine("File logs:");
            sb.AppendLine(textLogs);

            return Content(sb.ToString());
        }
    }
}
