using KissLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace KissLog_AspNetCore_20.ActionFilters
{
    public class TrackExecutionTimeAttribute : TypeFilterAttribute
    {
        public TrackExecutionTimeAttribute() : base(typeof(TrackExecutionTimeAttributeImpl))
        {
        }

        private class TrackExecutionTimeAttributeImpl : IActionFilter
        {
            private readonly ILogger _logger;
            private readonly Stopwatch _sw;
            private readonly Random _random;
            public TrackExecutionTimeAttributeImpl(
                ILogger logger)
            {
                _logger = logger;
                _sw = new Stopwatch();
                _random = new Random();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.Trace($"{nameof(TrackExecutionTimeAttribute)} begin");

                _sw.Restart();

                int sleep = _random.Next(100, 500);

                System.Threading.Thread.Sleep(sleep);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _sw.Stop();

                long elapsedMs = _sw.ElapsedMilliseconds;

                context.HttpContext.Response.Headers.Add("X-ElapsedMilliseconds", elapsedMs.ToString());

                _logger.Trace($"{nameof(TrackExecutionTimeAttribute)} complete. Took: {elapsedMs}ms.");
            }
        }
    }
}
