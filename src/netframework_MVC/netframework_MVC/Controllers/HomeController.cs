using KissLog;
using netframework_MVC.ActionFilters;
using netframework_MVC.Services;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace netframework_MVC.Controllers
{
    [TrackExecutionTime]
    public class HomeController : Controller
    {
        private readonly IKLogger _logger;
        private readonly IFooService _fooService;
        public HomeController()
        {
            _logger = Logger.Factory.Get();
            _fooService = new FooService(_logger);
        }

        public ActionResult Index()
        {
            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Critical("Critical message");
            _logger.Error(new NullReferenceException());

            _fooService.Foo();

            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];
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