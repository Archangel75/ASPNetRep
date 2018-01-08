using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lesson1.Models;
using Ninject;

namespace Lesson1.Controllers
{
    
    public class HomeController : Controller
    {
        [Inject]
        private IWeapon weapon { get; set; }

        public HomeController()
        {
            weapon = DependencyResolver.Current.GetService<IWeapon>();
        }

        public ActionResult Index()
        {
            return View(weapon);
        }
    }
}