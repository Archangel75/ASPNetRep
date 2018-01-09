using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson1.Modules
{
    public class TotalTimeModule : IHttpModule
    {
        private static float totalTime = 0;
        private static int requestCount = 0;

        public void Init(HttpApplication app)
        {
            IHttpModule module = app.Modules["Timer"];
            if (module != null && module is TimerModule)
            {
                TimerModule timerModule = (TimerModule)module;
                timerModule.RequestTimed += HandleRequestTimed;
            }
            app.EndRequest += HandleEndRequest;
        }

        private void HandleRequestTimed(object src, RequestTimerEventArgs e)
        {
            totalTime += e.Duration;
            requestCount++;
        }

        private void HandleEndRequest(object src, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            string result = string.Format("<div style='color:red;'>Количество обращений: {0} </div>" +
            "<div style='color:red;'>Общее время обработки запросов: {1:F5} секунд </div>",
            requestCount, totalTime);
            context.Response.Write(result);
        }

        public void Dispose()
        { }
    }
}