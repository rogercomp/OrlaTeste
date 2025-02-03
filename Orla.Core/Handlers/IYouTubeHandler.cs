using Orla.Core.Models.Youtube;
using Orla.Core.Requests.YouTube;
using Orla.Core.Responses;

namespace Orla.Core.Handlers;

public interface IYouTubeHandler
{
    Task<Response<List<YouTubeDetail>?>> GetVideosAsync(string? query, string? duration, DateTime? publishedAfter, DateTime? publishedBefore);

    Task<Response<YouTubeDetail>?> CreateVideoAsync(CreateYouTubeDetailRequest request);

    Task<Response<YouTubeDetail>?> UpdateVideoAsync(UpdateYouTubeDetailRequest request);

    Task<Response<YouTubeDetail>?> DeleteVideoAsync(DeleteYouTubeDetailRequest request);

}
