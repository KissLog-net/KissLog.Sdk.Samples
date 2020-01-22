using KissLog.Apis.v1.Listeners;
using KissLog.AspNet.Mvc;
using KissLog.AspNet.Web;
using KissLog.FlushArgs;
using KissLog.Listeners;
using KissLog.Samples.Mvc.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KissLog.Samples.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Add KissLog exception filter
            GlobalFilters.Filters.Add(new KissLogWebMvcExceptionFilterAttribute());

            ConfigureKissLog();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                var logger = Logger.Factory.Get();
                logger.Error(exception);

                if (logger.AutoFlush() == false)
                {
                    Logger.NotifyListeners(logger);
                }
            }
        }

        private void ConfigureKissLog()
        {
            // Register KissLog.net cloud listener
            KissLogConfiguration.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                ConfigurationManager.AppSettings["KissLog.OrganizationId"],
                ConfigurationManager.AppSettings["KissLog.ApplicationId"])
            )
            {
                ApiUrl = ConfigurationManager.AppSettings["KissLog.ApiUrl"]
            });

            // Register local text files listener
            KissLogConfiguration.Listeners.Add(new LocalTextFileListener(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"))
            {
                FlushTrigger = FlushTrigger.OnMessage // OnMessage | OnFlush
            });

            // Additional KissLog configuration
            KissLogConfiguration.Options
                .ShouldLogResponseBody((ILogListener listener, FlushLogArgs args, bool defaultValue) =>
                {
                    if (args.WebProperties.Request.Url.LocalPath == "/")
                        return true;

                    return defaultValue;
                })
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is ProductNotFoundException productNotFoundEx)
                    {
                        sb.AppendLine("ProductNotFoundException:");
                        sb.AppendLine($"ProductId = {productNotFoundEx.ProductId}");
                    }

                    return sb.ToString();
                });

            KissLogConfiguration.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
        }

        // Register HttpModule
        public static KissLogHttpModule KissLogHttpModule = new KissLogHttpModule();

        public override void Init()
        {
            base.Init();

            KissLogHttpModule.Init(this);
        }
    }
}
