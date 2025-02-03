using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Models.Youtube;
using Orla.Core.Requests.YouTube;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.YouTube;

public class CreateYouTubeDetailEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)  
    => app.MapPost("", HandleAsync)
            .WithName("Youtube: Create video youtube database")
            .WithSummary("Cria um video do youtube na base")
            .WithDescription("Cria um video do youtube na base")
            .WithOrder(2)
            .Produces<Response<YouTubeDetail?>>();

    private static async Task<IResult> HandleAsync(IYouTubeHandler handler,
                                                   CreateYouTubeDetailRequest request)
    {
        var result = await handler.CreateVideoAsync(request);

        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
