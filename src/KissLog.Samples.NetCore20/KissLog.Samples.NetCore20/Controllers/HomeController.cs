﻿using KissLog.Samples.NetCore20.ActionFilters;
using KissLog.Samples.NetCore20.Exceptions;
using KissLog.Samples.NetCore20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace KissLog.Samples.NetCore20.Controllers
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
            _logger.Info("Hello world from AspNetCore 2.0");

            var viewModel = new IndexViewModel
            {
                KissLogRequestLogsUrl = $"https://kisslog.net/RequestLogs/{_configuration["KissLog.ApplicationId"]}/kisslog-sample",
                LocalTextFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs")
            };

            return View(viewModel);
        }

        public IActionResult TriggerError()
        {
            Random random = new Random();
            int productId = random.Next(1, 10000);

            throw new ProductNotFoundException(productId);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}