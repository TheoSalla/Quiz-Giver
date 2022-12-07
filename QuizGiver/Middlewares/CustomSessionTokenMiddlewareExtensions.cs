namespace QuizGiver.Middlewares;

public static class CustomSessionTokenMiddlewareExtensions
{
    public static IApplicationBuilder UseQuizSessionToken(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomSessionTokenMiddleware>();
    }
}
