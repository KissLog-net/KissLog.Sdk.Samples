using NLog;
using NLog_AspNet_WebApi.ActionFilters;
using NLog_AspNet_WebApi.Exceptions;
using NLog_AspNet_WebApi.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace NLog_AspNet_WebApi.Controllers
{
    [TrackExecutionTime]
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

            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];

            var viewModel = new IndexViewModel
            {
                KissLogRequestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/nlog-aspnet-webapi",
                LocalTextFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            };

            return View(viewModel);
        }

        public ActionResult TriggerException()
        {
            Random random = new Random();
            int productId = random.Next(1, 10000);

            throw new ProductNotFoundException(productId);
        }
    }
}
