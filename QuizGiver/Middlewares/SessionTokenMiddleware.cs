using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizGiver.Middlewares
{
    public class CustomSessionTokenMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly Token _token;

        public CustomSessionTokenMiddleware(RequestDelegate next, Token token)
        {
            _next = next;
            _token = token;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("COOKE TIME!!");
            var sessionCookie = context.Request.Cookies["session_token"];
            if (sessionCookie == null)
            {
                Console.WriteLine("Settiing cookie if value is null!!");
                var cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(30),
                Secure = true    
                    
                };

                context.Response.Cookies.Append("session_token", _token.SessionToken);
            }
            // // before logic
            // after logic
            await _next(context);
        }
    }
    public static class CustomSessionTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseQuizSessionToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomSessionTokenMiddleware>();
        }
    }
}