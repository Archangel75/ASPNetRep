using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.Identity;
using MyReviewProject.Controllers;
using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers

{       
    public class HomeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var query = from r in Db.Reviews
                        join u in Db.Users on r.AuthorId equals u.Id into lj
                        from u in lj.DefaultIfEmpty()
                        join s in Db.Subjects on r.SubjectId equals s.SubjectId
                        orderby r.DateCreate descending
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
                            Username = u.UserName,
                            Subjectname = s.Name
                        };

            var reviews = await query.ToListAsync();
            var content = new IndexReviewViewModel
            {
                Reviews = reviews
            };

            return View(content);
        }
    }
}