using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userRole = context.User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        if (string.IsNullOrEmpty(userRole))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access Denied.");
            return;
        }

        await _next(context);
    }
}