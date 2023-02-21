using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace dotnetcore_3._1_WebApp.ActionFilters
{
    public class TrackExecutionTimeAttribute : TypeFilterAttribute
    {
        public TrackExecutionTimeAttribute() : base(typeof(TrackExecutionTimeAttributeImpl))
        {
        }

        private class TrackExecutionTimeAttributeImpl : IActionFilter
        {
            private readonly ILogger<TrackExecutionTimeAttributeImpl> _logger;
            private readonly Stopwatch _sw;
            private readonly Random _random;
            public TrackExecutionTimeAttributeImpl(
                ILogger<TrackExecutionTimeAttributeImpl> logger)
            {
                _logger = logger;
                _sw = new Stopwatch();
                _random = new Random();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.LogDebug("TrackExecutionTimeAttribute begin");

                _sw.Restart();

                int sleep = _random.Next(100, 500);

                System.Threading.Thread.Sleep(sleep);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _sw.Stop();

                context.HttpContext.Response.Headers.Add("X-ElapsedMilliseconds", _sw.ElapsedMilliseconds.ToString());

                _logger.LogDebug("TrackExecutionTimeAttribute complete. Took: {ElapsedMs}ms.", _sw.ElapsedMilliseconds);
            }
        }
    }
}
