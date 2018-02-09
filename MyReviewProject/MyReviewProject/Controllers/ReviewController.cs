using MyReviewProject.Models;
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
                //var catId = db.Categories.Where(c => c.Name.ToLower() == content.Category.ToLower())
                //                         .Select(c => c.Id).FirstOrDefault(0);
                //var subId = db.SubCategories.Where(s => s.CategoryId== catId)
                //                            .Select(s=>s.SubCategoryId).FirstOrDefault(0);
                //var subjectId = db.Subjects.Where(s => s.Name.ToLower() == content.Objectname.ToLower()).Select(s => s.SubjectId).FirstOrDefault(0);

                //review.SubjectId = subjectId;

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


        public  ActionResult AutocompleteSearch(string term)
        {

            var models = db.Subjects.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckExistSubject(string term)
        {
            var subjectId = db.Subjects.Where(s => s.Name.ToLower() == term.ToLower()).Select(s => s.SubjectId).FirstOrDefault();

            if (subjectId > 0)            
                return ViewBag.NoName = "";            
            else           
                return ViewBag.NoName = "Похоже никто ещё не делал обзор на это.";
            
        }
    }
}