namespace Miroku.Web.Services;

public class CookieService(IHttpContextAccessor httpContextAccessor)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public void SetCookie(string name, string value)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            return;
        }
        httpContext.Response.Cookies.Append(
            name,
            value,
            new CookieOptions
            {
                Expires = DateTimeOffset.MaxValue,
                HttpOnly = true
            });
    }

    public string GetCookie(string name)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var value = string.Empty;
        if(!httpContext?.Request?.Cookies?.TryGetValue(name, out value) ?? false)
        {
            return string.Empty;
        }

        return value ?? string.Empty;
    }
}
