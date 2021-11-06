using KissLog;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace KissLog_AspNet_WebApi.ActionFilters
{
    public class TrackExecutionTimeAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _sw;
        private readonly Random _random;
        public TrackExecutionTimeAttribute()
        {
            _sw = new Stopwatch();
            _random = new Random();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IKLogger logger = Logger.Factory.Get();

            logger.Trace("TrackExecutionTimeAttribute begin");

            _sw.Restart();

            int sleep = _random.Next(100, 500);

            System.Threading.Thread.Sleep(sleep);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            IKLogger logger = Logger.Factory.Get();

            _sw.Stop();

            long elapsedMs = _sw.ElapsedMilliseconds;

            filterContext.HttpContext.Response.Headers.Add("X-ElapsedMilliseconds", elapsedMs.ToString());

            logger.Trace($"TrackExecutionTimeAttribute complete. Took: {elapsedMs}ms.");
        }
    }
}