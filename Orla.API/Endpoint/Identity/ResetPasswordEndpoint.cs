using System.Security.Claims;
using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Requests.Account;

namespace Orla.Api.Endpoint.Identity;

public class ResetPasswordEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapPost("/reset-password", HandleAsync);

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IAccountHandler handler, ResetPasswordRequest req)
    {

        var request = new ResetPasswordRequest
        {
            Email = req.Email,
            Token = req.Token,
            Password = req.Password
        };

        var result = await handler.ResetPasswordAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);

    }
}
