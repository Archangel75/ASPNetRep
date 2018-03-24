﻿using Microsoft.AspNet.Identity.Owin;
using MyReviewProject.Controllers;
using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    public class ReviewController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> Review(int Id)
        {
            if (Id != null && Id != 0)
            {
                var query = from r in Db.Reviews
                            where r.ReviewId == Id
                            join u in Db.Users on r.AuthorId equals u.Id into lj
                            from u in lj.DefaultIfEmpty()
                            join s in Db.Subjects on r.SubjectId equals s.SubjectId
                            select new ShowReviewViewModel
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
                var review = await query.FirstAsync();
                review.Comments = await GetComments(Id);
                return View(review);
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<List<CommentsDTO>> GetComments(int ReviewId)
        {
            if (ReviewId > -1)
            {
                var query = from c in Db.Comments
                            join u in Db.Users on c.AuthorId equals u.Id
                            where c.ReviewId == ReviewId
                            orderby c.CreateTime descending
                            select new CommentsDTO
                            {
                                Id = c.Id,
                                Comment = c.Content,
                                Author = new UserDTO
                                {
                                    Id = u.Id,
                                    UserName = u.UserName
                                },
                                CreateTime = c.CreateTime,
                                Likes = c.Likes
                            };

                var commentList = await query.ToListAsync();
                return commentList;
            }
            return new List<CommentsDTO>();
        }

        [HttpPost]
        public async Task<ActionResult> PostComment(string comment, int id, int ReviewId)
        {
            int success = 1;
            if (comment != "" && ReviewId > 0)
            {
                ApplicationUser user = null;
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    user = await UserManager.FindByNameAsync(HttpContext.User.Identity.Name);
                }
                var model = new Comment()
                {
                    Content = comment,
                    CreateTime = DateTime.Now,
                    ReplyToId = id,
                    AuthorId = user.Id ?? "",
                    Likes = 0,
                    ReviewId = ReviewId
                };
                Db.Comments.Add(model);
                Db.SaveChanges();
                return Json(new { success }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]        
        public async Task<ActionResult> PostLike(int id, int ReviewId)
        {
            if (id != -1)
            {
                var query = from c in Db.Comments
                            where c.Id == id && c.ReviewId == ReviewId
                            select c;

                var comment = await query.FirstAsync();
                comment.Likes++;
                Db.Entry(comment).State = EntityState.Modified;
                await Db.SaveChangesAsync();
            }            

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create(CreateReviewViewModel review)
        {
            review.Categories = Db.Categories.ToList();
            ViewBag.subs = new List<SubCategory>(Db.SubCategories.Where(s => s.CategoryId == 1));
            return View(review);
        }

        public ActionResult GetSubCategories(string catname)
        {
            var catId = Db.Categories.Where(c => c.Name.ToLower() == catname.ToLower())
                                    .Select(c => c.Id).FirstOrDefault();
            var subs = Db.SubCategories.Where(s => s.CategoryId == catId);

            string responce = "";
            foreach (var sub in subs)
            {
                responce += String.Format("<li><label><input name=\"subCategory\" type=\"radio\" id=\"{0}\" value=\"{1}\" />{1}</label></li>", sub.SubCategoryId, sub.Name);
            }

            return Json(new { result = responce}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateSubject( string subcatname, string subjname)
        {
            var checkexist = Db.Subjects.Where(sub => sub.Name.ToLower() == subjname.ToLower()).FirstOrDefault();
            if (checkexist == null)
            {
                var subCatId = Db.SubCategories.Where(s => s.Name == subcatname)
                                       .Select(s => s.SubCategoryId).FirstOrDefault();
                if (subCatId != 0)
                {
                    Db.Subjects.Add(new Subject { AverageRating = 0, Name = subjname, SubCategoryId = subCatId });
                    Db.SaveChanges();
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateReviewViewModel content,  HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    SubjectId = Db.Subjects.Where(s => s.Name.ToLower() == content.Objectname.ToLower()).Select(s => s.SubjectId).FirstOrDefault(),
                    Rating = content.Rating,
                    Recommend = Convert.ToByte(content.Recomendations ? 1 : 0),
                    Exp = Convert.ToByte(content.Experience),
                    Like = content.Like,
                    Dislike = content.Dislike,
                    Content = content.Comment
            };

                Subject subject = Db.Subjects.Where(su => su.SubjectId == review.SubjectId).FirstOrDefault();
                if (subject.AverageRating == 0)
                {
                    subject.AverageRating = (double)content.Rating;
                }
                else
                {
                    double[] rating = Db.Reviews.Where(r => r.SubjectId == review.SubjectId).Select(r => r.Rating).ToArray();
                    subject.AverageRating = (rating.Sum() + content.Rating) / rating.Count()+1;
                }

                ApplicationUser user = null;
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    user = await UserManager.FindByNameAsync(HttpContext.User.Identity.Name);
                    review.AuthorId = user.Id;
                }

                if (uploadImage != null)
                {
                    byte[] imageData = null;                    
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    review.Image = imageData;
                }

                review.DateCreate = DateTime.Now;

                Db.Reviews.Add(review);
                await Db.SaveChangesAsync();
                
                
                return RedirectToAction("Index","Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public  ActionResult AutocompleteSearch(string term)
        {

            var models = Db.Subjects.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name, id = a.SubjectId, subcat = a.SubCategoryId })
                            .Distinct();

            return Json(Convert.ToString(models.GetType().GetProperty("value")), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckExistSubject(string term)
        {
            var subjectId = Db.Subjects.Where(s => s.Name.ToLower() == term.ToLower()).Select(s => s.SubjectId).FirstOrDefault();

            if (subjectId > 0)
                return Json(new { correct = true }, JsonRequestBehavior.AllowGet);
            //return ViewBag.NoName = "";            
            else
                return Json(new { correct = false }, JsonRequestBehavior.AllowGet);
            //return ViewBag.NoName = "Похоже никто ещё не делал обзор на это.";

        }        

    }
}