﻿using KissLog;
using KissLog.AspNet.Web;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using System.Configuration;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NLog_AspNet_WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureKissLog();
        }

        private void ConfigureKissLog()
        {
            // Register KissLog.net cloud listener
            KissLogConfiguration.Listeners.Add(new RequestLogsApiListener(new Application(
                ConfigurationManager.AppSettings["KissLog.OrganizationId"],
                ConfigurationManager.AppSettings["KissLog.ApplicationId"])
            )
            {
                ApiUrl = ConfigurationManager.AppSettings["KissLog.ApiUrl"]
            });

            KissLogConfiguration.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
        }

        public static KissLogHttpModule KissLogHttpModule = new KissLogHttpModule();

        public override void Init()
        {
            base.Init();

            KissLogHttpModule.Init(this);
        }
    }
}