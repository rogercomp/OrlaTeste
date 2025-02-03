using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Requests.Account;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.Identity;

public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapPost("/register-user", HandleAsync)
       .WithName("Users: Create")
       .WithSummary("Cria um novo usuário")
       .WithDescription("Cria um novo usuário")
       .WithOrder(1)
       .Produces<Response<IResult?>>();

    private static async Task<IResult> HandleAsync(
        IAccountHandler handler,
        RegisterRequest request)
    {
        var result = await handler.CreateAsync(request);

        return result.Succeeded
            ? TypedResults.Created($"/{result}", result)
            : TypedResults.BadRequest(result);
    }
}
