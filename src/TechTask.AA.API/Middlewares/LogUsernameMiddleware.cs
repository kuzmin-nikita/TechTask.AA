using Serilog.Context;

namespace TechTask.AA.API.Middlewares
{
    public class LogUsernameMiddleware
    {
        private readonly RequestDelegate next;

        public LogUsernameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var username = context.User.Identity.Name ?? "Unathorized user";

            LogContext.PushProperty("Username", username);

            return next(context);
        }
    }
}
