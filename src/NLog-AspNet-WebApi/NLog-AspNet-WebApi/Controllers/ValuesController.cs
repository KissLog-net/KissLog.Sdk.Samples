using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;

namespace NLog_AspNet_WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILogger _logger;
        public ValuesController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            _logger.Info("Hello world from NLog!");

            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Fatal("Fatal message");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
