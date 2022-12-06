namespace QuizGiver.Middlewares
{
    public class CustomSessionTokenMiddleware
    {

        private readonly RequestDelegate _next;

        public CustomSessionTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, Token token)
        {
            var sessionCookie = context.Request.Cookies["session_token"];
            if (sessionCookie == null)
            {
                await token.GenerateTokenAsync();
                var cookieOptions = new CookieOptions()
                {
                    Path = "/",
                    IsEssential = true,
                    HttpOnly = true,

                };
                context.Response.Cookies.Append("session_token", token.SessionToken);
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