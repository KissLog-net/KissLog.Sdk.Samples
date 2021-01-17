using KissLog;
using KissLog_AspNet_WebApi.ActionFilters;
using System.Collections.Generic;
using System.Web.Http;

namespace KissLog_AspNet_WebApi.Controllers
{
    [TrackExecutionTime]
    public class ValuesController : ApiController
    {
        private readonly ILogger _logger;
        public ValuesController()
        {
            _logger = Logger.Factory.Get();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            _logger.Info("Hello world from KissLog!");

            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Critical("Critical message");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
