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

        public ActionResult Index()
        {         

            return View(db.Categories);
        }
    }
}