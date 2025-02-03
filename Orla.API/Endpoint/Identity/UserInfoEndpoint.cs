using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Requests.Account;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.Identity;

public class UserInfoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/user-info/{id}", HandleAsync)
        .WithName("Users: Info")
        .WithSummary("Informações do usuário")
        .WithDescription("Informações do usuário")
        .WithOrder(3)
        .Produces<Response<IResult?>>();

    private static async Task<IResult?> HandleAsync(        
        IAccountHandler handler,
        long id
        )
    {

        var request = new GetUserByIdRequest
        {           
            Id = id
        };        

        var result = await handler.GetUserByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
