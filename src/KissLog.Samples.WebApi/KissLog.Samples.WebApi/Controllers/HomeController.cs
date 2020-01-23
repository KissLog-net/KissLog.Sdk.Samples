using KissLog.Samples.WebApi.ActionFilters;
using KissLog.Samples.WebApi.Exceptions;
using KissLog.Samples.WebApi.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace KissLog.Samples.WebApi.Controllers
{
    [TrackExecutionTime]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController()
        {
            _logger = Logger.Factory.Get();
        }

        public ActionResult Index()
        {
            _logger.Info("Hello world from AspNet.WebApi!");

            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];

            var viewModel = new IndexViewModel
            {
                KissLogRequestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-sample",
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
