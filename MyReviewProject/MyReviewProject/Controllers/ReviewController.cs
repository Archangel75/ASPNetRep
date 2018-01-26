using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    [Authorize]
    [Authorize(Roles = "Users")]
    public class ReviewController : Controller
    {
        ReviewContext db = new ReviewContext();

        public ActionResult Review(int Id)
        {
            var reviews = db.Reviews;
            ViewBag.Review = reviews;
            ViewBag.ReviewId = Id;
            return View(db.Reviews);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public Task<ActionResult> Create()
        //{
        //    return RedirectToAction("Index");
        //}
    }
}