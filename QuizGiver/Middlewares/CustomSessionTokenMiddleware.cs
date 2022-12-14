namespace QuizGiver.Middlewares
{
    sealed internal class CustomSessionTokenMiddleware
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
                context.Response.Cookies.Append("session_token", token.SessionToken);
            }
            // // before logic
            await _next(context);
            // after logic
        }
    }
}
