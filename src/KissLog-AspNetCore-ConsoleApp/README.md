# .NET Core ConsoleApp + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/70632bac-7c20-446e-9109-49b4f91203f2/kisslog-aspnetcore-consoleapp

**Program.cs**

```csharp
using KissLog;

namespace KissLog_AspNetCore_ConsoleApp
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

![kisslog.net](/src/KissLog-AspNetCore-ConsoleApp/KissLog-AspNetCore-ConsoleApp/KissLog-AspNetCore-ConsoleApp.png)