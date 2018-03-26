using Microsoft.AspNet.Identity.Owin;
using MyReviewProject.Models;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        
        public BaseController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        protected DateTime DefaultDatetime = new DateTime(1753, 01, 01);

        private ApplicationDbContext _db;
        public ApplicationDbContext Db
        {
            get
            {
                return _db ?? GetNewContext();
            }
            set
            {
                _db = value;
            }        
        }

        private ApplicationDbContext GetNewContext()
        {
            _db = ApplicationDbContext.Create();
            return _db;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            protected set
            {
                _signInManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        protected override void OnException(ExceptionContext error)
        {
            error.ExceptionHandled = true;
            int code = 0;
            if (error.Exception is HttpException)
            {
                code = ((HttpException)error.Exception).GetHttpCode();
            }

            switch (code)
            {
                case 400:
                case 401:
                case 404:
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    error.Result = View("~/Views/Error/NotFound.cshtml");
                    break;
                case 403:
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    error.Result = View("~/Views/Error/Forbidden.cshtml");
                    break;
                case 0:
                case 500:
                case 501:
                case 502:
                case 503:
                case 504:
                case 505:
                    ErrorModel model = new ErrorModel
                    {
                        Code = code,
                        Source = error.Exception.Source ?? error.RouteData.Values["controller"].ToString() + " " + error.RouteData.Values["action"].ToString(),
                        Message = error.Exception.Message,
                        Trace = error.Exception.StackTrace
                    };
                    var innerexception = error.Exception;
                    while (innerexception.InnerException != null)
                    {
                        model.AddStuff = innerexception.InnerException.Message + "\n   " + innerexception.InnerException.Source + " \n " + innerexception.InnerException.StackTrace.Replace(" at ", "\n >> at ") + " \n";
                        innerexception = innerexception.InnerException;
                    }

                    
                    ViewBag.Model = model;
                    error.Result = View("~/Views/Error/ServerError.cshtml");                       
                    break;
                default:
                    error.Result = View("~/Views/Error/NotFound.cshtml");
                    break;
            }
        }        
    }
}


