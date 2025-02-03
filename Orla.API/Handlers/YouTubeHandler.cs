using Microsoft.EntityFrameworkCore;
using Orla.Api.Data;
using Orla.Core;
using Orla.Core.Handlers;
using Orla.Core.Models.Youtube;
using Orla.Core.Requests.YouTube;
using Orla.Core.Responses;
using System.Text.Json;

namespace Orla.Api.Handlers;

public class YouTubeHandler(AppDbContext context, IHttpClientFactory httpClientFactory) : IYouTubeHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<YouTubeDetail>?> CreateVideoAsync(CreateYouTubeDetailRequest request)
    {
        try
        {
            var videoItem = await context.YouTubeDetails
                                       .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (videoItem is not null)
                return new Response<YouTubeDetail>(null, 404, "Video já existe na base de dados!");

            var video = new YouTubeDetail
            {
                Id = request.Id,
                Title = request.Title,
                Link = request.Link,
                Thumbnail = request.Thumbnail,
                PublishedAt = request.PublishedAt
            };

            await context.YouTubeDetails.AddAsync(video);
            await context.SaveChangesAsync();

            return new Response<YouTubeDetail>(video, 201, "Video criado com sucesso!");
        }
        catch
        {
            return new Response<YouTubeDetail>(null, 500, "Não foi possível salvar o vídeo!");
        }
    }

    public async Task<Response<YouTubeDetail>?> DeleteVideoAsync(DeleteYouTubeDetailRequest request)
    {
        try
        {
            var video = await context.YouTubeDetails
                                        .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (video is null)
                return new Response<YouTubeDetail>(null, 404, "Video não encontrado");

            context.YouTubeDetails.Remove(video);
            await context.SaveChangesAsync();

            return new Response<YouTubeDetail>(video, message: "Video excluído com sucesso!");
        }
        catch
        {
            return new Response<YouTubeDetail>(null, 500, "Não foi possível excluir o vídeo");
        }
    }

    public async Task<Response<List<YouTubeDetail>?>> GetVideosAsync(string? query, string? duration, DateTime? createdAfter, DateTime? createdBefore)
    {
        string baseUrl = "https://www.googleapis.com/youtube/v3/search";

        string urlBaseDetails = "https://www.googleapis.com/youtube/v3/videos";

        string qry = "manipulação de medicamentos";

        string publishedAfter = string.Empty;
        string publishedBefore = string.Empty;

        if (createdAfter is not null && createdBefore is not null)
        {
            publishedAfter =  createdAfter.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
            publishedBefore = createdBefore.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
        else
        {
            publishedAfter = "2022-01-01T00:00:00Z";
            publishedBefore = "2022-12-31T23:59:59Z";
        }

        string url = "";
        if (duration is not null && (duration.Equals("long") || duration.Equals("medium") || duration.Equals("short")))
        {
            url = $"{baseUrl}?part=snippet&q={Uri.EscapeDataString(qry)}&regionCode=BR&publishedAfter={publishedAfter}&publishedBefore={publishedBefore}&videoDuration={duration}&type=video&key={Configuration.ApiGoogleKey}";
        }
        else
        {
            url = $"{baseUrl}?part=snippet&q={Uri.EscapeDataString(qry)}&regionCode=BR&publishedAfter={publishedAfter}&publishedBefore={publishedBefore}&key={Configuration.ApiGoogleKey}";
        }

        HttpResponseMessage response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<YouTubeSearchResult>(jsonResponse);

            if (searchResult?.items != null)
            {
                var videolist = searchResult.items.Select(item => new YouTubeDetail
                {
                    Id = item.id.videoId,
                    Title = item.snippet?.title,
                    Link = $"https://www.youtube.com/watch?v={item.id.videoId}",
                    Thumbnail = item.snippet?.thumbnails?.medium?.url,
                    PublishedAt = item.snippet?.publishedAt
                });                

                if (query is not null) // Adicionado filtro para buscar videos que contenham a palavra "xxx" no título
                {
                    videolist = videolist.Where(x => x.Title!.Contains(query))
                                         .OrderByDescending(x => x.PublishedAt).ToList();
                }
                else
                {
                    videolist = videolist.OrderByDescending(x => x.PublishedAt);
                }

                return new Response<List<YouTubeDetail>?>(videolist.ToList(), 200, "Videos retornados com sucesso!");
            }
            else
            {
                return new Response<List<YouTubeDetail>?>(null, 404, "Erro ao buscar videos!");
            }
        }
        else
        {
            return new Response<List<YouTubeDetail>?>(null, 500, "Erro ao buscar videos!");
        }
    }

    public async Task<Response<YouTubeDetail>?> UpdateVideoAsync(UpdateYouTubeDetailRequest request)
    {
        try
        {
            var video = await context.YouTubeDetails
                                        .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (video is null)
                return new Response<YouTubeDetail>(null, 404, "Video não encontrado");

            video.Title = request.Title;
            video.Link = request.Link;
            video.Thumbnail = request.Thumbnail;

            context.YouTubeDetails.Update(video);
            await context.SaveChangesAsync();

            return new Response<YouTubeDetail>(video, message: "Video atualizado com sucesso");
        }
        catch
        {
            return new Response<YouTubeDetail>(null, 500, "Não foi possível alterar o video!");
        }
    }
}
