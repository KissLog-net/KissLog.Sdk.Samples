using KissLog.Apis.v1.Listeners;
using KissLog.Listeners;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace KissLog.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureKissLog();

            string intro = CreateIntro();
            Console.WriteLine(intro);

            ILogger logger = new Logger(url: "/ConsoleApp");
            logger.Info("Main begin");

            try
            {
                Console.WriteLine("Enter integer value for A:");
                string valueForA = Console.ReadLine();

                Console.WriteLine("Enter integer value for B:");
                string valueForB = Console.ReadLine();

                logger.Debug($"User input for A = {valueForA}");
                logger.Debug($"User input for B = {valueForB}");

                int a = int.Parse(valueForA);
                int b = int.Parse(valueForB);

                int sum = a + b;

                Console.WriteLine($"a + b = {sum}");

                double division = a / b;

                Console.WriteLine($"a / b = {division}");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                logger.Info("Main completed");

                Logger.NotifyListeners(logger);
            }

            Console.WriteLine("Demo completed");

            Console.ReadKey();
        }

        private static void ConfigureKissLog()
        {
            // Register KissLog.net cloud listener
            KissLogConfiguration.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
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
                FlushTrigger = FlushTrigger.OnMessage // OnMessage | OnFlush
            });

            // optional KissLog configuration
            KissLogConfiguration.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is DivideByZeroException divideByZeroException)
                    {
                        sb.AppendLine("DivideByZeroException");
                    }

                    return sb.ToString();
                });

            KissLogConfiguration.InternalLog = (message) =>
            {
                Console.WriteLine(message);
            };
        }

        private static string CreateIntro()
        {
            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];
            string requestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-sample";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("This ASP.NET Console application is using KissLog to save the logs on:");
            sb.AppendLine();
            sb.AppendLine("KissLog.net:");
            sb.AppendLine(requestLogsUrl);
            sb.AppendLine();
            sb.AppendLine("Local text files: ");
            sb.AppendLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
