using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson1.Handlers
{
    public class UserInfoHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string result = "<p>Ваш IP: " + context.Request.UserHostAddress + "</p>";
            result += "<p>UserAgent: " + context.Request.UserAgent + "</p>";
            context.Response.Write(result);
        }
        public bool IsReusable
        {
            get { return false; }
        }
    }
}