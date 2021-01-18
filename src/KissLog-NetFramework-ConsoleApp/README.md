# .NET Framework ConsoleApp + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/1958c78c-5fe0-48c5-99d0-84f083aa8c98/kisslog-netframework-consoleapp

**Program.cs**

```csharp
using KissLog;

namespace KissLog_NetFramework_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
```

**kisslog.net**

![kisslog.net](/src/KissLog-NetFramework-ConsoleApp/KissLog-NetFramework-ConsoleApp/KissLog-NetFramework-ConsoleApp.png)

Description:

ASP.NET ConsoleApp using KissLog to save logs to kisslog.net