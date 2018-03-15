using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.Identity;
using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<ActionResult> Index()
        {            
            var query = from r in db.Reviews
                        join u in db.Users on r.AuthorId equals u.Id into lj
                        from u in lj.DefaultIfEmpty()
                        orderby r.DateCreate
                        select new CustomReviewDTO
                        {
                            ReviewId = r.ReviewId,
                            Content = r.Content,
                            Dislike = r.Dislike,
                            Exp = r.Exp,
                            Like = r.Like,
                            Rating = r.Rating,
                            Image = r.Image,
                            Recommend = r.Recommend,
                            AuthorId = r.AuthorId
                        };

            var reviews = await query.ToListAsync();
            var content = new IndexReviewViewModel
            {
                Reviews = reviews
            };

            return View(content);

            //var list = (from r in db.Reviews
            //            orderby r.DateCreate
            //            select new { ReviewId = r.ReviewId, Content = r.Content, Dislike = r.Dislike, Exp = r.Exp, Like = r.Like, Rating = r.Rating, Image = r.Image, Recommend = r.Recommend, AuthorId = r.AuthorId }).ToList()
            //            .Select(c => new CustomReview {ReviewId = c.ReviewId, Content = c.Content, Dislike = c.Dislike, Exp = c.Exp, Like = c.Like, Rating = c.Rating, Image = c.Image, Recommend = c.Recommend, AuthorId = c.AuthorId });


            ////foreach (var r in list)
            ////{
            ////    if (r.AuthorId != null)
            ////    {
            ////        r.Username = dbc.Users.Where(u => u.Id == r.AuthorId).Select(u => u.UserName).FirstOrDefault();
            ////    }
            ////}

            //content.Reviews = list;
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