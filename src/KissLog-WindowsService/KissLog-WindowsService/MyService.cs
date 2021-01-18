using KissLog;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Listeners;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace KissLog_WindowsService
{
    partial class MyService : ServiceBase
    {
        private readonly Timer _timer = new Timer();
        private readonly int _triggerInterval = 1000;

        private ILogger Logger = new Logger();

        public MyService()
        {
            InitializeComponent();

            ConfigureKissLog();
        }

        protected override void OnStart(string[] args)
        {
            Logger = new Logger();

            Logger.Info("***** Starting service *****");

            _timer.Elapsed += new ElapsedEventHandler(Execute);
            _timer.Interval = _triggerInterval;
            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
            if (_timer != null)
                _timer.Enabled = false;

            Logger.Info("Service stopped successfully");
        }

        public void Execute(object source, ElapsedEventArgs e)
        {
            ILogger logger = new Logger(url: "MyService/Execute");

            try
            {
                logger.Info("Hello world from KissLog!");
                logger.Trace("Trace message");
                logger.Debug("Debug message");
                logger.Info("Info message");
                logger.Warn("Warning message");
                logger.Error("Error message");
                logger.Critical("Critical message");
            }
            finally
            {
                KissLog.Logger.NotifyListeners(logger);
            }
        }

        private static void ConfigureKissLog()
        {
            // optional KissLog configuration
            KissLogConfiguration.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            KissLogConfiguration.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners();
        }

        private static void RegisterKissLogListeners()
        {
            // multiple listeners can be registered using KissLogConfiguration.Listeners.Add() method

            // register KissLog.net cloud listener
            KissLogConfiguration.Listeners.Add(new RequestLogsApiListener(new Application(
                ConfigurationManager.AppSettings["KissLog.OrganizationId"],
                ConfigurationManager.AppSettings["KissLog.ApplicationId"])
            )
            {
                ApiUrl = ConfigurationManager.AppSettings["KissLog.ApiUrl"],
                UseAsync = false
            });

            // Register local text files listener
            KissLogConfiguration.Listeners.Add(new LocalTextFileListener(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"))
            {
                FlushTrigger = FlushTrigger.OnMessage
            });
        }

    }
}
