using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Requests.Account;

namespace Orla.Api.Endpoint.Identity;

public class LoginEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
       => app.MapPost("/login-user", HandleAsync);

    
    private static async Task<IResult> HandleAsync(LoginRequest request, IAccountHandler handler)
    {
        var result = await handler.LoginAsync(request);

        return result.Flag
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);        
    }
}
