# .NET Core ConsoleApp + KissLog &#8680; kisslog.net

**Program.cs**

```csharp
using KissLog;

namespace KissLog_AspNetCore_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
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