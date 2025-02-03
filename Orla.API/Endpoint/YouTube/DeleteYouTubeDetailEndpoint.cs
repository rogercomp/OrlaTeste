using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Models.Youtube;
using Orla.Core.Requests.YouTube;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.YouTube;

public class DeleteYouTubeDetailEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Exclui uma transação")
            .WithDescription("Exclui uma transação")
            .WithOrder(4)
            .Produces<Response<YouTubeDetail?>>();

    private static async Task<IResult> HandleAsync(IYouTubeHandler handler,
                                                   string id)
    {
        var request = new DeleteYouTubeDetailRequest
        {            
            Id = id
        };

        var result = await handler.DeleteVideoAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
