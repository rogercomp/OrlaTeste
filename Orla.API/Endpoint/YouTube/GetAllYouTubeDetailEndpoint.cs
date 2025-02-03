using Microsoft.AspNetCore.Mvc;
using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Models.Youtube;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.YouTube;

public class GetAllYouTubeDetailEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)        
      => app.MapGet("/", HandleAsync)
            .WithName("Youtube Search")
            .WithSummary("Obtem videos youtube")
            .WithDescription("Obtem videos youtube")
            .WithOrder(1)
            .Produces<Response<List<YouTubeDetail>?>>();

    private static async Task<IResult> HandleAsync(IYouTubeHandler handler,
                                                  [FromQuery] string? title,
                                                  [FromQuery] string? duration,
                                                  [FromQuery] string? author,
                                                  [FromQuery] DateTime? createdAfter,
                                                  [FromQuery] DateTime? createdBefore,
                                                  [FromQuery] string? q)
    {
        var result = await handler.GetVideosAsync(q, duration, createdAfter, createdBefore);

        return result.IsSuccess
            ? TypedResults.Ok(result.Data)
            : TypedResults.BadRequest(result);
    }
}
