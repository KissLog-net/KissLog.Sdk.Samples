using KissLog;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace netframework_MVC.ActionFilters
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

            filterContext.RequestContext.HttpContext.Response.Headers.Add("X-ElapsedMilliseconds", _sw.ElapsedMilliseconds.ToString());

            logger.Trace($"TrackExecutionTimeAttribute complete. Took: {_sw.ElapsedMilliseconds}ms.");
        }
    }
}