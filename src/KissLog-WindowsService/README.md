# WindowsService + KissLog ---> kisslog.net

https://kisslog.net/RequestLogs/9a4d0f95-0969-4205-a5f5-3e62d3a51d78/kisslog-windowsservice

**Program.cs**

```csharp
using KissLog;

namespace KissLog_WindowsService
{
    partial class MyService : ServiceBase
    {
        private readonly Timer _timer = new Timer();
        private readonly int _triggerInterval = 1000;

        public MyService()
        {
            InitializeComponent();

            ConfigureKissLog();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += new ElapsedEventHandler(Execute);
            _timer.Interval = _triggerInterval;
            _timer.Enabled = true;
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

        protected override void OnStop()
        {
            if (_timer != null)
                _timer.Enabled = false;
        }
    }
}
```

**kisslog.net**

![kisslog.net](/src/KissLog-WindowsService/KissLog-WindowsService/KissLog-WindowsService.png)