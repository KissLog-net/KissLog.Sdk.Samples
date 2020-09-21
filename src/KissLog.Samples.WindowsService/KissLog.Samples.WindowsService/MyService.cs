using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Listeners;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.ServiceProcess;
using System.Timers;

namespace KissLog.Samples.WindowsService
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
            ILogger logger = new Logger(url: "MyService.Execute");

            try
            {
                logger.Info("GET https://example.com/ begin");

                using (HttpClient client = new HttpClient())
                {
                    using (var response = client.GetAsync("https://example.com/").Result)
                    {
                        var stringResponse = response.Content.ReadAsStringAsync().Result;

                        logger.Debug($"StatusCode: {response.StatusCode}");
                        logger.Debug($"Response length: {stringResponse.Length}");
                    }
                }

                logger.Info("GET https://example.com/ complete");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                KissLog.Logger.NotifyListeners(logger);
            }
        }

        private static void ConfigureKissLog()
        {
            // Register KissLog.net cloud listener
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

            KissLogConfiguration.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
        }
    }
}
