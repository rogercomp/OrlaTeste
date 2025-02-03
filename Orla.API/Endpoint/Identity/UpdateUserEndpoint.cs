using System.Security.Claims;
using Orla.Api.Common;
using Orla.Core.DTOs;
using Orla.Core.Handlers;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.Identity;

public class UpdateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapPut("user/{id}", HandleAsync)
            .WithName("User: Update")
            .WithSummary("Atualiza um usuario")
            .WithDescription("Atualiza um usuario")
            .WithOrder(2)
            .Produces<Response<string?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IAccountHandler handler,
        UserDTO request,
        long id)
    {
        request.Id = id;
        var result = await handler.UpdateUserAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
