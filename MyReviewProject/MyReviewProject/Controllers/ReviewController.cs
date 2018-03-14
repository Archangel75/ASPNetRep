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
            ViewBag.subs = new List<SubCategory>(db.SubCategories.Where(s => s.CategoryId == 1));
            return View(review);
        }

        public ActionResult GetSubCategories(string catname)
        {
            var catId = db.Categories.Where(c => c.Name.ToLower() == catname.ToLower())
                                    .Select(c => c.Id).FirstOrDefault();
            var subs = db.SubCategories.Where(s => s.CategoryId == catId);

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
            var checkexist = db.Subjects.Where(sub => sub.Name.ToLower() == subjname.ToLower()).FirstOrDefault();
            if (checkexist == null)
            {
                var subCatId = db.SubCategories.Where(s => s.Name == subcatname)
                                       .Select(s => s.SubCategoryId).FirstOrDefault();
                if (subCatId != 0)
                {
                    db.Subjects.Add(new Subject { AverageRating = 0, Name = subjname, SubCategoryId = subCatId });
                    db.SaveChanges();
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateReviewViewModel content,  HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review();

                review.SubjectId = db.Subjects.Where(s => s.Name.ToLower() == content.Objectname.ToLower()).Select(s => s.SubjectId).FirstOrDefault();
                review.Rating = content.Rating;

                Subject subject = db.Subjects.Where(su => su.SubjectId == review.SubjectId).FirstOrDefault();
                if (subject.AverageRating == 0)
                {
                    subject.AverageRating = (double)content.Rating;
                }
                else
                {
                    double[] rating = db.Reviews.Where(r => r.SubjectId == review.SubjectId).Select(r => r.Rating).ToArray();
                    subject.AverageRating = (rating.Sum() + content.Rating) / rating.Count()+1;
                }
                

                review.Recommend = Convert.ToByte(content.Recomendations ? 1 : 0);
                review.Exp = Convert.ToByte(content.Experience);
                review.Like = content.Like;
                review.Dislike = content.Dislike;
                review.Content = content.Comment;


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
                return RedirectToAction("~Home/Index");
            }

            return RedirectToAction("~Home/Index");
        }


        public  ActionResult AutocompleteSearch(string term)
        {

            var models = db.Subjects.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name, id = a.SubjectId, subcat = a.SubCategoryId })
                            .Distinct();

            return Json(Convert.ToString(models.GetType().GetProperty("value")), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckExistSubject(string term)
        {
            var subjectId = db.Subjects.Where(s => s.Name.ToLower() == term.ToLower()).Select(s => s.SubjectId).FirstOrDefault();

            if (subjectId > 0)
                return Json(new { correct = true }, JsonRequestBehavior.AllowGet);
            //return ViewBag.NoName = "";            
            else
                return Json(new { correct = false }, JsonRequestBehavior.AllowGet);
            //return ViewBag.NoName = "Похоже никто ещё не делал обзор на это.";

        }
    }
}