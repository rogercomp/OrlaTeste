using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orla.Api.Common;
using Orla.Api.Models;

namespace Orla.Api.Endpoint.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapPost("/logout-user", HandleAsync)
        .RequireAuthorization();


    private static async Task<IResult> HandleAsync(HttpContext context, SignInManager<User> signInManager, [FromBody] object empty)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            Console.WriteLine("Logout API");
            if (empty != null)
            {
                Console.WriteLine("Realizando logout API");
                await signInManager.SignOutAsync();
                return Results.Ok();
            }
        }

        return Results.Unauthorized();
    }
}
