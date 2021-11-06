using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using KissLog.Listeners.FileListener;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace KissLog_AspNetCore_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.SetFactory(new KissLog.LoggerFactory(new Logger(url: "ConsoleApp/Main")));

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            string intro = CreateIntro(configuration);
            Console.WriteLine(intro);

            ConfigureKissLog(configuration);

            ILogger logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogTrace("Trace log");
            logger.LogDebug("Debug log");
            logger.LogInformation("Information log");
            logger.LogWarning("Warning log");
            logger.LogError("Error log");
            logger.LogCritical("Critical log");

            var loggers = Logger.Factory.GetAll();
            Logger.NotifyListeners(loggers);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(logging =>
            {
                logging
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddKissLog(options =>
                    {
                        options.Formatter = (FormatterArgs args) =>
                        {
                            string message = args.DefaultValue;

                            if (args.Exception == null)
                                return message;

                            string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(message);
                            sb.Append(exceptionStr);

                            return sb.ToString();
                        };
                    });
            });
        }

        private static void ConfigureKissLog(IConfiguration configuration)
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

            RegisterKissLogListeners(configuration);
        }

        private static void RegisterKissLogListeners(IConfiguration configuration)
        {
            // multiple listeners can be registered using KissLogConfiguration.Listeners.Add() method

            // register KissLog.net cloud listener
            KissLogConfiguration.Listeners.Add(new RequestLogsApiListener(new Application(
                configuration["KissLog.OrganizationId"],
                configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"],
                UseAsync = false
            });

            // Register local text files listener
            KissLogConfiguration.Listeners.Add(new LocalTextFileListener("logs", FlushTrigger.OnMessage));
        }

        private static string CreateIntro(IConfiguration configuration)
        {
            string applicationId = configuration["KissLog.ApplicationId"];
            string requestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-aspnetcore-consoleapp";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(".NET Core ConsoleApp + KissLog ---> kisslog.net");
            sb.AppendLine();
            sb.AppendLine("This .NET Core ConsoleApp is using KissLog to write the logs on:");
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
