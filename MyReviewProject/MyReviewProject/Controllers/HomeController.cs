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
        
        
        public ActionResult Index(ApplicationDbContext context)
        {
            var users = context.Users.ToList();
            
            ViewBag.users = context.Users.ToList();
            return View();
        }
    }
}