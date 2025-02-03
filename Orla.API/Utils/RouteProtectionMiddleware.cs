namespace Orla.Api.Utils;

public class RouteProtectionMiddleware
{
    private readonly RequestDelegate _next;
    public RouteProtectionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? path = context.Request.Path;
        if (path != null && RouteConfig.HiddenRoutes.Contains(path))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
        await _next(context);
    }
}
