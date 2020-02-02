using System.ServiceProcess;

namespace KissLog.Samples.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
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
    }
}
