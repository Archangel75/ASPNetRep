using SimpleBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBookStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext context = new BookContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = context.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public ViewResult Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            context.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            context.SaveChanges();
            ViewData["Name"] = purchase.Person;
            return View("~/Views/Home/ThxForPurchase.cshtml");
        }
    }
}