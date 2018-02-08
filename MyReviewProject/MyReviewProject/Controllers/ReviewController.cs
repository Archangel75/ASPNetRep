﻿using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    public class ReviewController : Controller
    {
        ReviewContext db = new ReviewContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Review(int Id)
        {
            var reviews = db.Reviews;
            ViewBag.Review = reviews;
            ViewBag.ReviewId = Id;
            return View(db.Reviews);
        }


        [HttpGet]
        public ActionResult Create(CreateReviewViewModel review)
        {
            review.Categories = db.Categories.ToList();
            review.subCategories = db.SubCategories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateReviewViewModel content, Review review, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var catId = db.Categories.Where(c => c.Name.ToLower() == content.Category.ToLower()).Select(c => c.Id).FirstOrDefault();
                var subId = db.SubCategories.Where(s => s.CategoryId== catId).Select(s=>s.SubCategoryId).FirstOrDefault();
                var subject = db.Subjects.Where(s => s.SubCategoryId == subId && s.Name.ToLower() == content.Objectname.ToLower());
                if (subject == null)
                {
                    db.Subjects.Add(new Subject {Name=content.Objectname, SubCategoryId = Convert.ToInt32(subId) });
                }

                review.Rating = content.Rating;
                review.Recomendation = content.Recomendations ? 1 : 0;
                review.Experience = content.Experience;



                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    if (uploadImage.ContentType != "image")
                    {
                        ModelState.AddModelError("Прикреплённый файл не является картинкой!", "Ошибка");
                    }
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    review.Image = imageData;
                }

                review.DateCreate = DateTime.Now;

                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}