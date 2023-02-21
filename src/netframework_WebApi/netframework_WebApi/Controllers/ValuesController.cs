using KissLog;
using netframework_WebApi.ActionFilters;
using netframework_WebApi.Services;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Http;

namespace netframework_WebApi.Controllers
{
    [TrackExecutionTime]
    public class ValuesController : ApiController
    {
        private readonly IKLogger _logger;
        private readonly IFooService _fooService;
        public ValuesController()
        {
            _logger = Logger.Factory.Get();
            _fooService = new FooService(_logger);
        }

        public string Get()
        {
            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Critical("Critical message");
            _logger.Error(new NullReferenceException());

            _fooService.Foo();

            string applicationId = ConfigurationManager.AppSettings["KissLog.ApplicationId"];
            string logsUrl = $"https://kisslog.net/RequestLogs/{applicationId}/kisslogsampleapp";
            string textLogs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

            var sb = new StringBuilder();
            sb.AppendLine("KissLog.net logs:");
            sb.AppendLine(logsUrl);
            sb.AppendLine();
            sb.AppendLine("File logs:");
            sb.AppendLine(textLogs);

            return sb.ToString();
        }
    }
}
