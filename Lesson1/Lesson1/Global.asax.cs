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

        public MvcApplication()
        {
            BeginRequest += (src, args) => AddEvent("BeginRequest");
            AuthenticateRequest += (src, args) => AddEvent("AuthentucateRequest");
            PreRequestHandlerExecute += (src, args) => AddEvent("PreRequestHandlerExecute");
            ReleaseRequestState += (src, args) => AddEvent("ReleaseRequestState");
            EndRequest += (src, args) => AddEvent("EndRequest");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
