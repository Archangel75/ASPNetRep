using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    public class ErrorController : Controller
    {
        //// GET: Error
        //public ActionResult NotFound()
        //{
        //    Response.StatusCode = (int)HttpStatusCode.NotFound;
        //    return View();
        //}

        //public ActionResult Unauthorized()
        //{
        //    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //    return View();
        //}

        //public ActionResult Forbidden()
        //{
        //    Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //    return View();
        //}

        //public ActionResult ServerError()
        //{

        //    return View();
        //}

        //public ActionResult ErrorHandler(ExceptionContext error)
        //{
        //    error.ExceptionHandled = true;
        //    int code = 0;
        //    if (error.Exception is HttpException)
        //    {
        //        code = ((HttpException)error.Exception).GetHttpCode();
        //    }            

        //    switch (code)
        //    {
        //        case 400:
        //        case 401:
        //        case 404:
        //            Response.StatusCode = (int)HttpStatusCode.NotFound;
        //            return View("NotFound");
        //        case 403:
        //            Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //            return View("Forbidden");
        //        case 500:
        //        case 501:
        //        case 502:
        //        case 503:
        //        case 504:
        //        case 505:
        //            ErrorModel model = new ErrorModel();
        //            model.Code = code;
        //            model.Source = error.RouteData.Values["controller"].ToString() + " " + error.RouteData.Values["action"].ToString();
        //            model.Message = error.Exception.Message;
        //            model.Trace = error.Exception.StackTrace;
        //            return View("ServerError");
        //        default:
        //            return View("NotFound");
        //    }
        //    return View("Error");
        //}
    }
}