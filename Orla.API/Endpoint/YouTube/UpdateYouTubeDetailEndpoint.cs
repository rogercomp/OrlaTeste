using Orla.Api.Common;
using Orla.Core.Handlers;
using Orla.Core.Models.Youtube;
using Orla.Core.Requests.YouTube;
using Orla.Core.Responses;

namespace Orla.Api.Endpoint.YouTube;

public class UpdateYouTubeDetailEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapPut("", HandleAsync)
            .WithName("Videos: Update")
            .WithSummary("Atualiza o video")
            .WithDescription("Atualiza o video")
            .WithOrder(3)
            .Produces<Response<YouTubeDetail?>>();

    private static async Task<IResult> HandleAsync(IYouTubeHandler handler,                                                   
                                                   UpdateYouTubeDetailRequest request)
    {
        var result = await handler.UpdateVideoAsync(request);

        return result.IsSuccess
           ? TypedResults.Ok(result)
           : TypedResults.BadRequest(result);
    }
}
