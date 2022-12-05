public class CookieSet
{
    private readonly RequestDelegate _next;
 
    private HttpContext _context;
    public CookieSet(RequestDelegate next)
    {
        _next = next;

    }

    public async Task Invoke(HttpContext context)
    {
        _context = context;
        context.Response.OnStarting(OnStartingCallBack);
        await _next.Invoke(context);
    }

    private Task OnStartingCallBack()
    {
        var cookieOptions = new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddHours(1),
            IsEssential = true,
            HttpOnly = false,
            Secure = false,

        };
        _context.Response.Cookies.Append("MyCookie", "TheValue", cookieOptions);
        return Task.FromResult(0);
    }
}