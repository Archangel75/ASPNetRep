using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Lesson1.Modules
{
    public class RequestTimerEventArgs : EventArgs
    {
        public float Duration { get; set; }
    }
    public class TimerModule : IHttpModule
    {
        private Stopwatch timer;
        public event EventHandler<RequestTimerEventArgs> RequestTimed;
        public void Init(HttpApplication app)
        {
            app.BeginRequest += HandleBeginRequest;
            app.EndRequest += HandleEndRequest;
        }
        private void HandleBeginRequest(object src, EventArgs args)
        {
            timer = Stopwatch.StartNew();
        }
        private void HandleEndRequest(object src, EventArgs args)
        {
            HttpContext context = HttpContext.Current;
            float duration = ((float)timer.ElapsedTicks) / Stopwatch.Frequency;
            context.Response.Write(string.Format(
                "<div style='color:red;'>Время обработки запроса: {0:F5} секунд</div>", duration));
            if (RequestTimed != null)
            {
                RequestTimed(this,
                new RequestTimerEventArgs { Duration = duration });
            }
        }

        public void Dispose()
        {

        }
    }
}