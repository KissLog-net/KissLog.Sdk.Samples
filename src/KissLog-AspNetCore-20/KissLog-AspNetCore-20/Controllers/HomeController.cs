using KissLog;
using KissLog_AspNetCore_20.ActionFilters;
using KissLog_AspNetCore_20.Exceptions;
using KissLog_AspNetCore_20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace KissLog_AspNetCore_20.Controllers
{
    [TrackExecutionTime]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public HomeController(
            IConfiguration configuration,
            ILogger logger)
        {
            _configuration = configuration;
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

            string applicationId = _configuration["KissLog.ApplicationId"];

            var viewModel = new IndexViewModel
            {
                KissLogRequestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-aspnetcore-2x",
                LocalTextFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            };

            return View(viewModel);
        }

        public IActionResult TriggerException()
        {
            Random random = new Random();
            int productId = random.Next(1, 10000);

            throw new ProductNotFoundException(productId);
        }
    }
}
