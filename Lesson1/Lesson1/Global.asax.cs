using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lesson1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {

            logger.Info("Application Start");
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // обработка события BeginRequest
        protected void Application_BeginRequest()
        {
            AddEvent("BeginRequest");
        }
        // обработка события AuthenticateRequest
        protected void Application_AuthenticateRequest()
        {
            AddEvent("AuthenticateRequest");
        }
        // обработка события PreRequestHandlerExecute
        protected void Application_PreRequestHandlerExecute()
        {
            AddEvent("PreRequestHandlerExecute");
        }
        private void AddEvent(string name)
        {
            List<string> eventList = Application["events"] as List<string>;
            if (eventList == null)
            {
                Application["events"] = eventList = new List<string>();
            }
            eventList.Add(name);
        }
    }
}
