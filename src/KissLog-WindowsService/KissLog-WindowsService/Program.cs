using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;

namespace KissLog_WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string intro = CreateIntro();
            Debug.WriteLine(intro);

            #if DEBUG

            ExecuteDebug();
            return;

            #endif

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MyService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static void ExecuteDebug()
        {
            var service = new MyService();
            service.Execute(null, null);
        }

        private static string CreateIntro()
        {
            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];
            string requestLogsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslog-windowsservice";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("WindowsService + KissLog ---> kisslog.net");
            sb.AppendLine();
            sb.AppendLine("This WindowsService is using KissLog to write the logs on:");
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
