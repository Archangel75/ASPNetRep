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

        protected int subCatId = 0;
        protected int subjectId = 0;

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
                subCatId = db.SubCategories.Where(s => s.Name == subcatname)
                                       .Select(s => s.SubCategoryId).FirstOrDefault();
                if (subCatId != 0)
                {
                    db.Subjects.Add(new Subject { AverageRating = 0, Name = subjname, SubCategoryId = subCatId });
                    db.SaveChanges();
                    subjectId = db.Subjects.Where(s => s.Name == subjname).Select(s => s.SubjectId).FirstOrDefault();
                    subCatId = db.Subjects.Where(s => s.SubjectId == subjectId).Select(s => s.SubCategoryId).FirstOrDefault();
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateReviewViewModel content,  HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                Review review = new Models.Review();

                if (subjectId == 0)
                {
                    subjectId = db.Subjects.Where(s => s.Name.ToLower() == content.Objectname.ToLower()).Select(s => s.SubjectId).FirstOrDefault();
                }
                if (subCatId == 0)
                {
                    subCatId = db.Subjects.Where(s=> s.SubjectId == subjectId).Select(s=> s.SubCategoryId).FirstOrDefault();                    
                }    
                
                review.SubjectId = subjectId;
                review.Rating = content.Rating;

                Subject subject = db.Subjects.Where(su => su.SubjectId == subjectId).FirstOrDefault();
                if (subject.AverageRating == 0)
                {
                    subject.AverageRating = content.Rating;
                }
                else
                {
                    var rating = db.Reviews.Where(r => r.SubjectId == subjectId).Select(r => r.Rating);
                    subject.AverageRating = (rating.Sum() + content.Rating) / rating.Count();
                }
                

                review.Recommend = content.Recomendations ? 1 : 0;
                review.Exp = content.Experience;
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
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public  ActionResult AutocompleteSearch(string term)
        {

            var models = db.Subjects.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name, id = a.SubjectId, subcat = a.SubCategoryId })
                            .Distinct();
            subjectId = Convert.ToInt32(models.GetType().GetProperty("id"));
            subCatId = Convert.ToInt32(models.GetType().GetProperty("subcat"));

            return Json(Convert.ToString(models.GetType().GetProperty("value")), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckExistSubject(string term)
        {
            subjectId = db.Subjects.Where(s => s.Name.ToLower() == term.ToLower()).Select(s => s.SubjectId).FirstOrDefault();

            if (subjectId > 0)
                return Json(new { correct = true }, JsonRequestBehavior.AllowGet);
            //return ViewBag.NoName = "";            
            else
                return Json(new { correct = false }, JsonRequestBehavior.AllowGet);
            //return ViewBag.NoName = "Похоже никто ещё не делал обзор на это.";

        }
    }
}