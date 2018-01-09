using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lesson1.Models;
using Ninject;

namespace Lesson1.Controllers
{
    
    public class HomeController : Controller
    {
        public string Index()
        {
            List<string> events = HttpContext.Application["events"] as List<string>;
            string result = "<ul>";
            foreach (string e in events)
                result += "<li>" + e + "</li>";
            result += "</ul>";
            return result;
        }
    }
}