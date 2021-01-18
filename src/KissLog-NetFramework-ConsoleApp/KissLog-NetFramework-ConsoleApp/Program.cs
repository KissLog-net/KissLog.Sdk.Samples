using KissLog;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Listeners;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace KissLog_NetFramework_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string intro = CreateIntro();
            Console.WriteLine(intro);

            ConfigureKissLog();

            ILogger logger = new Logger(url: "/Program/Main");

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
                Logger.NotifyListeners(logger);
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
                Console.WriteLine(message);
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

        private static string CreateIntro()
        {
            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];
            string requestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-netframework-consoleapp";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(".NET Framework ConsoleApp + KissLog ---> kisslog.net");
            sb.AppendLine();
            sb.AppendLine("This .NET Framework Core ConsoleApp is using KissLog to write the logs on:");
            sb.AppendLine();
            sb.AppendLine("* Local text files: ");
            sb.AppendLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
            sb.AppendLine();
            sb.AppendLine("* kisslog.net:");
            sb.AppendLine(requestLogsUrl);
            sb.AppendLine();

            return sb.ToString();
        }

    }
}
