using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.Identity;
using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    
    public class HomeController : Controller
    {
        ReviewContext db = new ReviewContext();

        List<string> boopers = new List<string>();        
        
        
        public ActionResult Index(ControllerContext context)
        {
            ApplicationUser user = new ApplicationUser();
            ViewBag.users = user.Logins.ToList();
            return View();
        }
    }
}