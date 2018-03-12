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

        [HttpGet]
        public ActionResult Index(IndexReviewViewModel content)
        {
            //how to do this????
            var list = (from r in db.Reviews
                        (r.AuthorId == null ? join au in db.Users on r.AuthorId equals au.Id : join u in db.AspUsers on r.AuthorId equals u.Id)
                        join u in db.Users on r.AuthorId equals u.Id
                        orderby r.DateCreate
                        select new CustomReview { Content=r.Content, Dislike=r.Dislike, Exp=r.Exp, Like=r.Like, Rating=r.Rating, Image=r.Image, Recommend=r.Recommend, Username=r.User.UserName }).ToList();            

            content.Reviews = list;

            return View(content);
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Users?", HttpContext.User.IsInRole("Users"));

            return dict;
        }
    }
}